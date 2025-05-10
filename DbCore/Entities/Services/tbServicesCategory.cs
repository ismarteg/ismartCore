using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Entities.Services
{
    public class tbServicesCategory:BaseEntity
    {
        [MaxLength(300)]
        public string Name { get; set; } = "";
        [MaxLength(500)]
        public string? Description { get; set; }
        
        public string? EInvoiceCode { get; set; }



        [ForeignKey(nameof(tbServicesCategory))]
        public int? ParentId { get; set; }
        public tbServicesCategory? Parent { get; set; }
        public List<tbServicesCategory>? Children { get; set; }
    }
    
}
