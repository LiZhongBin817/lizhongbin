using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using System.Collections;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CDWM_MR.Common.Helper
{
   public class OfficeHelper
    {

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="lists"></param>
        /// <param name="head">中文列名对照</param>
        /// <param name="workbookFile">保存路径</param>
        public static MemoryStream getExcel<T>(List<T> lists, Hashtable head)
        {
            try
            {
                IWorkbook workbook = new XSSFWorkbook();
                NpoiMemoryStream ms = new NpoiMemoryStream();
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);
                bool h = false;
                int j = 1;
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();

                foreach (T item in lists)
                {
                    IRow dataRow = sheet.CreateRow(j);
                    int i = 0;
                    foreach (PropertyInfo column in properties)
                    {
                        if (!h)
                        {
                            headerRow.CreateCell(i).SetCellValue(head[column.Name] == null ? column.Name : head[column.Name].ToString());
                            dataRow.CreateCell(i).SetCellValue(column.GetValue(item, null) == null ? "" : column.GetValue(item, null).ToString());
                        }
                        else
                        {
                            dataRow.CreateCell(i).SetCellValue(column.GetValue(item, null) == null ? "" : column.GetValue(item, null).ToString());
                        }

                        i++;
                    }
                    h = true;
                    j++;
                }
                ms.AllowClose = false;
                workbook.Write(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                ms.AllowClose = true;
                //ms.= false;
                //sheet = null;
                //headerRow = null;
                return ms;
                //workbook = null;
                //MemoryStream fs = new MemoryStream();
                //byte[] data = ms.ToArray();
                //fs.Write(data, 0, data.Length);
                //File.WriteAllBytes(workbookFile, data);
                //fs.Flush();
                //fs.Close();
                //data = null;
                //ms = null;
                //fs = null;
            }
            catch (Exception ee)
            {
               
                return null;

            }
        }

    }

    public class NpoiMemoryStream : MemoryStream
    {
        public NpoiMemoryStream()
        {
            AllowClose = true;
        }

        public bool AllowClose { get; set; }

        public override void Close()
        {
            if (AllowClose)
                base.Close();
        }
    }
}
