using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class CoreBusiness
    {
       // [Logging(BusinessName = "预定房间")]
        public static void Describe(string memberName,string roomNumber)
        {
            Console.WriteLine(String.Format("尊敬的会员{0}，恭喜您预定房间{1}成功！", memberName, roomNumber), "提示");
        }
    }
    class LoggingHelper
    {
        private const string _errorLogFilePath = @"log.txt";
        public static void WriteLog(string message)
        {
            StreamWriter sw = new StreamWriter(_errorLogFilePath,true);
            string logContent = String.Format("[{0}]{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message);
            sw.WriteLine(logContent);
            sw.Flush();
            sw.Close();
        }
    }
    //[Serializable]
    //[AttributeUsage(AttributeTargets.Method,AllowMultiple = true,Inherited = true)]
    public sealed class LoggingAttribute 
    {
        public string BusinessName { get; set;  }
        public  void OnEntry()
        {
            LoggingHelper.WriteLog(BusinessName + "开始执行");
        }
        public  void OnExit()
        {
            LoggingHelper.WriteLog(BusinessName + "完成执行");
        }
    }
}
