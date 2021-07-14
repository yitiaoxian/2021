using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MqttClientConnection
{
    public interface IDataPublisher
    {        
        public void PublishShockWavePosition(string data);
        public void PublishSensorsData(string data);
        public void Close();
    }
}
