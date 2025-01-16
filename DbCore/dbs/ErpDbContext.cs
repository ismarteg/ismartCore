using DbCore.Entities.Clients;
using Microsoft.EntityFrameworkCore;


namespace DbCore.dbs
{
    public class ErpDbContext:Db_BaseContext
    {

        public ErpDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<tbClient> tbClient {  get; set; }
    }
   
}
