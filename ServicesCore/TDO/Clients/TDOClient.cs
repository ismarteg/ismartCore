using DbCore.Entities.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCore.TDO.Clients
{
    public class TDOClient:TDOBase
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string? NationalID { get; set; }
        [MaxLength(1000)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)]
        public string? Region { get; set; }
        [MaxLength(100)]
        public string? phone { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }

      
        public int? ReferedById { get; set; }
        public TDOClient ReferedBy { get; set; }
    }
}
