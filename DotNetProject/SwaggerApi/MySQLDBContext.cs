using Microsoft.EntityFrameworkCore;
using SwaggerApi.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerApi
{
    /// <summary>
    /// MySQL数据库连接上下文
    /// </summary>
    public class MySQLDBContext : DbContext
    {
        /// <summary>
        /// 表People的映射
        /// </summary>
        public DbSet<Person> People { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options"></param>
        public MySQLDBContext(DbContextOptions<MySQLDBContext> options) : base(options)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
