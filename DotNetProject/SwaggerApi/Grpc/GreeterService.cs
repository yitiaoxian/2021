using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApi.Grpc
{
    /// <summary>
    /// public static class ApiRequestExtensions
    /// </summary>
    public class GreeterService : Greeter.GreeterBase
    {
        /// <summary>
        /// public GreeterService()
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        ///  
        /// </summary>
        /// <param name="logger"></param>
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }
        //public override Task<HelloReply> SayHello(HelloRequest request,ServerCallContext context)
        //{
        //    return Task.FromResult(new HelloReply
        //    {
        //        Message = "Hello" + request.Name
        //    });
        //}
    }
}
