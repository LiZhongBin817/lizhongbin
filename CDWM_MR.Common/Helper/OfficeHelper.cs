

/****************************************************************************
*CLR版本:4.0.30319.42000
*机器名称:
*命名空间:HX.Common
*文件名:OfficeHelper
*版本号:V1.0.0.0
*唯一标识:$guidl0$
*当前的用户域:DESKTOP-TJDC7TV
*创建人:ZK
*电子邮箱:2226975012@qq.com
*创建时间:2019/4/13 16:26:09

*描述
*
*
*修改时间:2019/4/13 16:26:09
* 修改人:ZK
*版本号:V1.0.0.0
*描述:
*
****************************************************************************/

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="lists"></param>
        /// <param name="head">中文列名对照</param>
        /// <param name="workbookFile">Excel所在路径</param>
        /// <returns></returns>
        public static List<T> fromExcel<T>(Hashtable head, string workbookFile)
        {
            try
            {
                IWorkbook hssfworkbook;
                List<T> lists = new List<T>();
                using (FileStream file = new FileStream(workbookFile, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
                ISheet sheet = hssfworkbook.GetSheetAt(0) as XSSFSheet;
                IEnumerator rows = sheet.GetRowEnumerator();
                IRow headerRow = sheet.GetRow(0) as XSSFRow;
                int cellCount = headerRow.LastCellNum;
                //Type type = typeof(T);
                PropertyInfo[] properties;
                T t = default(T);
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    XSSFRow row = sheet.GetRow(i) as XSSFRow;
                    t = Activator.CreateInstance<T>();
                    properties = t.GetType().GetProperties();
                    foreach (PropertyInfo column in properties)
                    {
                        int j = headerRow.Cells.FindIndex(delegate (ICell c)
                        {
                            return c.StringCellValue == (head[column.Name] == null ? column.Name : head[column.Name].ToString());
                        });
                        if (j >= 0 && row.GetCell(j) != null)
                        {
                            object value = valueType(column.PropertyType, row.GetCell(j).ToString());
                            column.SetValue(t, value, null);
                        }
                    }
                    lists.Add(t);
                }
                return lists;
            }
            catch (Exception ee)
            {
                //myLog.Error("读取Excel中的数据时发生错误，可能Excel数据格式与指定格式不一致。" + ee.Message + "\r\n");
                return null;
            }
        }
        static object valueType(Type t, string value)
        {
            object o = null;
            string strt = "String";
            if (t.Name == "Nullable`1")
            {
                strt = t.GetGenericArguments()[0].Name;
            }
            switch (strt)
            {
                case "Decimal":
                    o = decimal.Parse(value);
                    break;
                case "Int":
                    o = int.Parse(value);
                    break;
                case "Float":
                    o = float.Parse(value);
                    break;
                case "DateTime":
                    o = DateTime.Parse(value);
                    break;
                default:
                    o = value;
                    break;
            }
            return o;
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
