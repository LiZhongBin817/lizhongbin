using CDWM_MR.IServices;
using CDWM_MR.Model.Models;
using Microsoft.Extensions.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Core.Tasks
{
    public class AutoCreateBook : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly Iv_b_bookinfoServices _Iv_b_bookinfoServices;
        List<v_b_bookinfo> bookdata;

        // 这里可以注入
        public AutoCreateBook(Iv_b_bookinfoServices v_b_bookinfoService)
        {
            _Iv_b_bookinfoServices = v_b_bookinfoService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("AutoCreateBook is starting.");
            //Console.WriteLine($"定时器线程ID--------{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()}--------");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));//一个小时

            return Task.CompletedTask;
        }
        public async void Querydata()
        {
            bookdata = await _Iv_b_bookinfoServices.Query();
        }
        private void DoWork(object state)
        {
            Querydata();
            var bookName = "books";
            var rootPath = "c:/bookExcels/";
            if (System.IO.Directory.Exists(rootPath) == false)
                System.IO.Directory.CreateDirectory(rootPath);

            var newFile = rootPath + bookName + ".xls";
            if (System.IO.File.Exists(newFile))
            {
                try
                {
                    System.IO.File.Delete(newFile);
                }
                catch (Exception ex)
                {
                    Dispose();
                }
            }
            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                var sheet = workbook.CreateSheet("orders");
                var header = sheet.CreateRow(0);
                header.CreateCell(0).SetCellValue("抄表册编号");
                header.CreateCell(1).SetCellValue("抄表册名称");
                header.CreateCell(2).SetCellValue("关联用户数量");
                header.CreateCell(3).SetCellValue("抄表员名称");
                header.CreateCell(4).SetCellValue("抄表员编号");
                header.CreateCell(5).SetCellValue("区域名称");
                var rowIndex = 1;
                foreach (var item in bookdata)
                {
                    var datarow = sheet.CreateRow(rowIndex);
                    datarow.CreateCell(0).SetCellValue(item.bookno);
                    datarow.CreateCell(1).SetCellValue(item.bookname);
                    datarow.CreateCell(2).SetCellValue(item.contectusernum.ToString());
                    datarow.CreateCell(3).SetCellValue(item.mrreadername);
                    datarow.CreateCell(4).SetCellValue(item.mrreadernumber);
                    datarow.CreateCell(5).SetCellValue(item.regionname);
                    rowIndex++;
                }
                workbook.Write(fs);
                var memory = new MemoryStream();
                using (var stream = new FileStream(newFile, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("AutoCreateBook is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
