using DbCore.Entities.Clients;
using Microsoft.EntityFrameworkCore;


namespace DbCore.dbs
{
    public class ClinicDbContext:Db_BaseContext
    {

        public ClinicDbContext(DbContextOptions options) : base(options)
        {
        }
        
    }
   
}
