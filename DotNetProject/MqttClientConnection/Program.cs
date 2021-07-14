using System;
using System.Collections.Generic;
using System.Threading;
using Common;
using Newtonsoft.Json;

namespace MqttClientConnection
{
    class Program
    {
        private static MqttClientHelper mqttHelper;
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Parse("2010-05-30T13:39:01.085");
            DateTime endTime = DateTime.Now;
            string brokerHostName = AppConfigurtaionServices.Configuration["MqttOption:BrokerHostName"];
            string brokerPort = AppConfigurtaionServices.Configuration["MqttOption:BrokerPort"];
            string brokerUserName = AppConfigurtaionServices.Configuration["MqttOption:BrokerUserName"];
            string brokerPassword = AppConfigurtaionServices.Configuration["MqttOption:BrokerPassword"];
            string[] topics = new string[] { "ShockWaveData" };
           

            string prefix = "http://localhost:51262/API/Monitoring/GetTestRunNosForMining";
            string suffix = $"?startTime={startTime}&endTime={endTime}";
            var runNos = HttpClientHelper.GetHttpResponse(prefix+suffix);
            ComWebResponseEntity runNoList = JsonConvert.DeserializeObject<ComWebResponseEntity>(runNos);
            List<object> runNoL = JsonConvert.DeserializeObject<List<object>>(runNoList.Content.ToString());
            
            mqttHelper = new MqttClientHelper(brokerHostName, Int32.Parse(brokerPort), brokerUserName, brokerPassword, topics);
            mqttHelper.Connect();
            foreach (object runNo in runNoL)
            {
                mqttHelper.MqttMsgPublish("TestStop", runNo.ToString());
                Console.WriteLine(runNo.ToString());
                Thread.Sleep(5000);
            }
            Console.ReadLine();
        }
    }
}
