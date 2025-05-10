using DbCore.Entities.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Entities.Clients
{
    public class tbClintesServices:BaseEntity
    {
        [ForeignKey(nameof(tbClient))]
        public int? ClientId { get; set; }
        public tbClient Client { get; set; }
        
        [ForeignKey(nameof(tbServices))]
        public int? ServiceId { get; set; }
        public tbServices? Service { get; set; }
        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    
    }
}
