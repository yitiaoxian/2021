using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common;
using MqttClientConnection.Practise;
using Newtonsoft.Json;

namespace MqttClientConnection
{
    class Program
    {
        private static MqttClientHelper mqttHelper;
        delegate void MyDel(int value);//声明委托类型
        static void Main(string[] args)
        {
            MultiThread multi = new MultiThread();
            multi.DoRun();
            Class1 class1 = new Class1();
            class1.DoRun();
            #region
            //DateTime startTime = DateTime.Parse("2010-05-30T13:39:01.085");
            //DateTime endTime = DateTime.Now;
            //string brokerHostName = AppConfigurtaionServices.Configuration["MqttOption:BrokerHostName"];
            //string brokerPort = AppConfigurtaionServices.Configuration["MqttOption:BrokerPort"];
            //string brokerUserName = AppConfigurtaionServices.Configuration["MqttOption:BrokerUserName"];
            //string brokerPassword = AppConfigurtaionServices.Configuration["MqttOption:BrokerPassword"];
            //string[] topics = new string[] { "ShockWaveData" };


            //string prefix = "http://localhost:51262/API/Monitoring/GetTestRunNosForMining";
            //string suffix = $"?startTime={startTime}&endTime={endTime}";
            //var runNos = HttpClientHelper.GetHttpResponse(prefix+suffix);
            //ComWebResponseEntity runNoList = JsonConvert.DeserializeObject<ComWebResponseEntity>(runNos);
            //List<object> runNoL = JsonConvert.DeserializeObject<List<object>>(runNoList.Content.ToString());

            //mqttHelper = new MqttClientHelper(brokerHostName, Int32.Parse(brokerPort), brokerUserName, brokerPassword, topics);
            //mqttHelper.Connect();
            //foreach (object runNo in runNoL)
            //{
            //    mqttHelper.MqttMsgPublish("TestStop", runNo.ToString());
            //    Console.WriteLine(runNo.ToString());
            //    Thread.Sleep(5000);
            //}
            #endregion
            MyDownloadString mds = new MyDownloadString();
            mds.DoRun();
            MyAsyncDownloadString asyncDownloadString = new();
            asyncDownloadString.DoRun();
            Program program = new ();
            MyDel myDel;//声明委托变量
            //
            Random rand = new Random();
            int randNum = rand.Next(0, 99);
            myDel = randNum < 50 ? new MyDel(program.PrintLow) : new MyDel(program.PrintHigh);
            myDel(randNum);
            Console.ReadLine();
        }
        void PrintLow(int value)
        {
            Console.WriteLine("{0} - low value",value);
        }
        void PrintHigh(int value)
        {
            Console.WriteLine("{0} - high value", value);
        }
    }
    class MyDownloadString
    {
        Stopwatch sw = new Stopwatch();
        public void DoRun()
        {
            const int LargeNumber = 6000000;
            sw.Start();
            int t1 = CountCharacters(1, "http://www.microsoft.com");
            int t2 = CountCharacters(2, "http://www.illustratedcsharp.com");
            CountToALargeNumber(1, LargeNumber);
            CountToALargeNumber(2, LargeNumber);
            CountToALargeNumber(3, LargeNumber);
            CountToALargeNumber(4, LargeNumber);

            Console.WriteLine("chars in http://www.microsoft.com    :{0}",t1);
            Console.WriteLine("chars in http://www.illustratedcsharp.com   :{0}", t2);
        }
        private int CountCharacters(int id ,string urlString)
        {
            WebClient client = new WebClient();
            Console.WriteLine("Starting call {0}    :   {1} ms",id,sw.Elapsed.TotalMilliseconds);
            string result = client.DownloadString(new Uri(urlString));
            Console.WriteLine(" Call {0} Completed :    {1} ms",id,sw.Elapsed.TotalMilliseconds);
            return result.Length;
        }
        private void CountToALargeNumber(int id ,int value)
        {
            for (long i = 0; i < value; i++) ;
            
            Console.WriteLine(" End counting    {0}  :    {1} ms", id, sw.Elapsed.TotalMilliseconds);
        }
    }
    class MyAsyncDownloadString
    {
        Stopwatch sw = new Stopwatch();
        public void DoRun()
        {
            const int LargeNumber = 6000000;
            sw.Start();
            Task<int> t1 = CountCharactersAsync(1, "http://www.microsoft.com");
            Task<int> t2 = CountCharactersAsync(2, "http://www.illustratedcsharp.com");
            CountToALargeNumber(1, LargeNumber);
            CountToALargeNumber(2, LargeNumber);
            CountToALargeNumber(3, LargeNumber);
            CountToALargeNumber(4, LargeNumber);

            Console.WriteLine("chars in http://www.microsoft.com    :{0}", t1.Result);
            Console.WriteLine("chars in http://www.illustratedcsharp.com   :{0}", t2.Result);
        }
        private async Task<int> CountCharactersAsync(int id, string urlString)
        {
            WebClient client = new WebClient();
            Console.WriteLine("Starting call {0}    :   {1} ms", id, sw.Elapsed.TotalMilliseconds);
            string result = await client.DownloadStringTaskAsync(new Uri(urlString));
            Console.WriteLine(" Call {0} Completed :    {1} ms", id, sw.Elapsed.TotalMilliseconds);
            return result.Length;
        }
        private void CountToALargeNumber(int id, int value)
        {
            for (long i = 0; i < value; i++) ;            
            Console.WriteLine(" End counting    {0}  :    {1} ms", id, sw.Elapsed.TotalMilliseconds);
        }
    }
}
