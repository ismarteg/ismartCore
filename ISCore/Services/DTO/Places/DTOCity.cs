using ISCore.Entities.Places;
using ISCore.Services.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Services.DTO.Places
{
    public class DTOCity : DTOBase<Guid>
    {
        public string Title { get; set; }
        public Guid CountryId { get; set; }
        public DTOCountry Country { get; set; }
    }
}
