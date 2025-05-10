using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Entities.Services
{
    public class tbServices:BaseEntity
    {
        [MaxLength(400)]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? EInvoiceCode { get; set; }
        

        [ForeignKey(nameof(tbServicesCategory))]
        public int? CategoryId { get; set; }
        public tbServicesCategory? Category { get; set; }


        
    }
}
