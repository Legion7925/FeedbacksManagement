using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    /// <summary>
    /// کلاس لاگ کردن خطاهای کنترل نشده برنامه
    /// </summary>
    public static class AppLogger
    {
        private static readonly object lockForFileWrit = new object();
        /// <summary>
        /// نوشتن خطای رخ داده در یک فایل تکست
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="ex"></param>
        public static void WriteLog(string pm, Exception ex)
        {
            Task.Run(() =>
            {
                lock (lockForFileWrit)
                {
                    string todayPath = string.Format("{0}/App-Logs/{1}", Environment.CurrentDirectory, DateTime.Now.Date.ToString("yyyy/MM/dd"));
                    string errorFilePath = string.Format("{0}/{1}.txt", todayPath, DateTime.Now.Hour);
                    if (!Directory.Exists(todayPath)) System.IO.Directory.CreateDirectory(todayPath);
                    var wr = $"\n{DateTime.Now.ToShortTimeString()}\n{pm}\n{ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}\n";
                    File.AppendAllText(errorFilePath, wr);
                }

            });

        }
        public static void WriteLog(string pm, string fileName)
        {
            Task.Run(() =>
            {
                lock (lockForFileWrit)
                {
                    string todayPath = string.Format("{0}/App-Logs/{1}", Environment.CurrentDirectory, DateTime.Now.Date.ToString("yyyy/MM/dd"));
                    string errorFilePath = string.Format("{0}/{1}.txt", todayPath, fileName);
                    if (!Directory.Exists(todayPath)) System.IO.Directory.CreateDirectory(todayPath);
                    var wr = $"{DateTime.Now.ToShortTimeString()}\n{pm}\n";
                    File.AppendAllText(errorFilePath, wr);
                }

            });

        }
    }
}
