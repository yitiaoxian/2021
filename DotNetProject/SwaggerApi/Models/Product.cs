using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApi.Models
{
    /// <summary>
    /// 产品测试
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id {  get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double Price {  get; set; }
    }
}
