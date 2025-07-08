using ISCore.Entities.Places;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.DTO.Places
{
    public class DTORegion:DTOBase
    {
        public string Title { get; set; }
        [ForeignKey(nameof(tbCity))]
        public int CityId { get; set; }
        public tbCity City { get; set; }
    }
}
