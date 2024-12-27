using DbCore.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.dbs
{
    public class ErpDbContext:Db_BaseContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
        {
        }
        public DbSet<tbClient> tbClient {  get; set; }
    }
   
}
