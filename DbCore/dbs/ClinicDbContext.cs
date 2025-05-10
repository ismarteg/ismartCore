using DbCore.Entities.Clients;
using DbCore.Entities.Clinic;
using Microsoft.EntityFrameworkCore;


namespace DbCore.dbs
{
    public class ClinicDbContext:Db_BaseContext
    {

        public ClinicDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<tblClinicData> tblClinicData { get; set; }
        DbSet<tblPatient> tblPatients { get; set; }

    }
   
}
