using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApi.Implements
{
    /// <summary>
    /// 
    /// </summary>
    public class TestServiceA : ITestService
    {
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string MultiServicesTest(string str)
        {
            return $"this is a test {str}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public async Task MultiServicesTestAsync(string str)
        {
            await new Task(() =>
            {
                //DO ALL THE HEAVY LIFTING!!!
            });
            return;
        }
    }
}
