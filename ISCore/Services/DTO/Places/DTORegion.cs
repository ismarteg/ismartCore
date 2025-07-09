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
    public class DTORegion : DTOBase<Guid>
    {
        public string Title { get; set; }
        [ForeignKey(nameof(tbCity))]
        public int CityId { get; set; }
        public tbCity City { get; set; }
    }
}
