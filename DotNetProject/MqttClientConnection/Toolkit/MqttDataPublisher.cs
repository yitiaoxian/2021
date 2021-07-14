using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace MqttClientConnection
{
    public class MqttDataPublisher : IDataPublisher
    {
        private MqttClientHelper mqttHelper;
        private readonly IConfiguration Configuration;

        public MqttDataPublisher(IConfiguration configuration)
        {
            string[] topics = new string[] { "ShockWaveData", "TestStop" };
            Configuration = configuration;
            string brokerHostName = Configuration["MqttOption:BrokerHostName"];
            string brokerPort = Configuration["MqttOption:BrokerPort"];
            string brokerUserName = Configuration["MqttOption:BrokerUserName"];
            string brokerPassword = Configuration["MqttOption:BrokerPassword"];
            mqttHelper = new MqttClientHelper(brokerHostName, Int32.Parse(brokerPort), brokerUserName, brokerPassword, topics);
            mqttHelper.MqttMessageReceived += MqttHelper_MqttMessageReceived;
            mqttHelper.MqttException += MqttHelper_MqttException;
        }

        private void MqttHelper_MqttMessageReceived(object sender, MqttMessageReceivedEventArgs e)
        {
            //处理采集程序发送的超扩段传感器位置，进行激波位置计算及预测
            if (e.Topic.Equals("ShockWaveData"))
            {
                
            }
            //处理车次结束后采集程序发送的车次号，进行自动化分析
            if (e.Topic.Equals("TestStop"))
            {                
                
            }
        }

        private void MqttHelper_MqttException(object sender, MqttExceptionEventArgs e)
        {
            //LogHelper.Error(e.Message);
        }

        public void PublishShockWavePosition(string data)
        {
            mqttHelper.MqttMsgPublish("ShockWavePosition", data);
        }
        /// <summary>
        /// 用于前端显示异常传感器信息
        /// </summary>
        /// <param name="data"></param>
        public void PublishSensorsData(string data)
        {
            mqttHelper.MqttMsgPublish("SensorsStatus",data);
        }       

        public void Close()
        {
            if (mqttHelper != null)
            {
                mqttHelper.Disconnect();
            }
        }
    }    
}
