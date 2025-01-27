using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.dbs
{
    public class ClinicDb(DbContextOptions options) : Db_BaseContext(options)
    {


    }
}
