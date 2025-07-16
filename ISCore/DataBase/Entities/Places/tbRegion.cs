using ISCore.Entities.Places;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Entities.Places
{
    [Table(nameof(tbRegion), Schema = "main")]
    public class tbRegion : GuidBaseEntity
    {
        public string Title { get; set; }
        [ForeignKey(nameof(tbCity))]
        public Guid CityId { get; set; }
        public tbCity City { get; set; }
    }
}
