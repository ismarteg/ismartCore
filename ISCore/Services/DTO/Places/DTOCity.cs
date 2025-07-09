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

        [ForeignKey(nameof(tbCountry))]
        public int CountryId { get; set; }
        public tbCountry Country { get; set; }
    }
}
