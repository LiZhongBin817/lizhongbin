using CDWM_MR.Common.Helper;
using CDWM_MR.IServices.Content;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CDWM_MR.Tasks
{
    public class JobWorkTime1 : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly Iv_taskinfoServices taskinfoServices;

        public JobWorkTime1(Iv_taskinfoServices iv_) {
            taskinfoServices = iv_;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
               TimeSpan.FromDays(30));//一个月
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            Console.WriteLine($"定时器线程ID--------{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()}--------");

            try
            {
                var model = await taskinfoServices.AutoCreat();
                Console.WriteLine($"BlogArticle:{model}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }

            ConsoleHelper.WriteSuccessLine($"Job 1： {DateTime.Now}");
        }

        public Task StopAsync(CancellationToken cancellationToken)


        {
            Console.WriteLine("Job 1 is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
