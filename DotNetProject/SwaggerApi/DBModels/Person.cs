using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApi.DBModels
{
    /// <summary>
    /// MySQL数据库实体
    /// </summary>
    [Table("people")]
    public class Person
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
