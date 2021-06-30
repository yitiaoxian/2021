using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApi.Implements
{
    /// <summary>
    /// 
    /// </summary>
    public class TestServiceB : ITestService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public  string MultiServicesTest(string str)
        {            
            return $"MultiServicesTest TestServiceB :{str}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Task MultiServicesTestAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
