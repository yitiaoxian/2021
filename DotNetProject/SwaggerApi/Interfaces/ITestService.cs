using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApi
{
    /// <summary>
    /// 测试服务接口
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// 多个服务异步测试
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Task MultiServicesTestAsync(string str);
        /// <summary>
        /// 多个服务同步测试
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string MultiServicesTest(string str);

    }
}
