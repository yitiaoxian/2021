using System;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MqttClientConnection
{
    public class MqttClientHelper
    {
        private const int testConnectionInterval = 60000;
        private Timer testConnectionTimer;
        private string brokerUserName;
        private string brokerPassword;
        private MqttClient mqttClient;

        public event EventHandler<MqttConnectionStatusChangedEventArgs> MqttConnectionStatusChanged;
        public event EventHandler<MqttMessageReceivedEventArgs> MqttMessageReceived;
        public event EventHandler<MqttExceptionEventArgs> MqttException;

        public MqttClientHelper(string hostName, int port, string userName, string password, string[] topics)
        {
            brokerUserName = userName;
            brokerPassword = password;

            //创建客户端实例
            mqttClient = new MqttClient(hostName, port, false, null, null, MqttSslProtocols.None);

            // 注册消息接收处理事件
            mqttClient.MqttMsgPublishReceived += new MqttClient.MqttMsgPublishEventHandler(MqttMsgPublishReceived);

            // 订阅主题,消息质量为2 
            byte[] qosLevels = new byte[topics.Length];
            for (int index = 0; index < topics.Length; index++)
            {
                qosLevels[index] = MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE;
            }
            mqttClient.Subscribe(topics, qosLevels);

            this.testConnectionTimer = new Timer(new TimerCallback(state => TestRemoteServerConnection(hostName)), null, 5000, testConnectionInterval);
        }

        private void TestRemoteServerConnection(string ipAddress)
        {
            Ping pingSender = new Ping();
            pingSender.PingCompleted += new PingCompletedEventHandler(PingSender_PingCompleted);
            try
            {
                pingSender.SendAsync(ipAddress, null);
            }
            catch (Exception ex)
            {
                OnMqttException(new MqttExceptionEventArgs { Source = ex.Source, Message = String.Format("与主机{0}的网络连接发生异常[{1}]", ipAddress, ex.Message) });

                Disconnect();
            }
            //LogHelper.Info("Mqtt连接状态："+mqttClient.IsConnected);
        }

        private void PingSender_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                Connect();
            }
            else
            {
                //OnMqttException(new MqttExceptionEventArgs { Source = "Network", Message = "测试网络连接失败" });
                Disconnect();
            }
        }

        private void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            OnMqttMessageReceived(new MqttMessageReceivedEventArgs { Topic = e.Topic, Message = Encoding.UTF8.GetString(e.Message) });
        }

        /// <summary>
        /// 发布消息到主题,消息质量为2,不保留
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="publishString"></param>
        public void MqttMsgPublish(string topic, string publishString)
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Publish(topic, Encoding.UTF8.GetBytes(publishString), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
        }

        public void Connect()
        {
            if (!mqttClient.IsConnected)
            {
                try
                {                    
                    //生成客户端ID并连接服务器
                    string clientId = Guid.NewGuid().ToString();
                    byte code = mqttClient.Connect(clientId, brokerUserName, brokerPassword);
                    if (code == 0)
                    {
                        OnMqttConnectionStatusChanged(new MqttConnectionStatusChangedEventArgs { ConnectionStatus = true });
                        string[] topics = new string[] { "ShockWaveData", "TestStop" };
                        // 订阅主题,消息质量为2 
                        byte[] qosLevels = new byte[topics.Length];
                        for (int index = 0; index < topics.Length; index++)
                        {
                            qosLevels[index] = MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE;
                        }
                        mqttClient.Subscribe(topics, qosLevels);
                        //LogHelper.Info("Mqtt连接成功");
                    }
                }
                catch (Exception ex)
                {
                    OnMqttException(new MqttExceptionEventArgs { Source = ex.Source, Message = String.Format("与MQTT Broker连接发生异常[{0}]", ex.Message) });
                }
            }
        }

        public void Disconnect()
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Disconnect();

                OnMqttConnectionStatusChanged(new MqttConnectionStatusChangedEventArgs { ConnectionStatus = false });
            }
        }

        protected virtual void OnMqttConnectionStatusChanged(MqttConnectionStatusChangedEventArgs e)
        {
            MqttConnectionStatusChanged?.Invoke(this, e);
        }

        protected virtual void OnMqttMessageReceived(MqttMessageReceivedEventArgs e)
        {
            MqttMessageReceived?.Invoke(this, e);
            
        }

        protected virtual void OnMqttException(MqttExceptionEventArgs e)
        {
            MqttException?.Invoke(this, e);
        }
    }

    public class MqttConnectionStatusChangedEventArgs : EventArgs
    {
        public bool ConnectionStatus { get; set; }
    }

    public class MqttMessageReceivedEventArgs : EventArgs
    {
        public string Topic { get; set; }
        public string Message { get; set; }
    }

    public class MqttExceptionEventArgs : EventArgs
    {
        public string Source { get; set; }
        public string Message { get; set; }
    }
}
