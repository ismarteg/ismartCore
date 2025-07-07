using DbCore.Entities.Places;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Entities.Places
{
    public class tbCity: GuidBaseEntity
    {
        public string Title { get; set; }

        [ForeignKey(nameof(tbCountry))]
        public int CountryId { get; set; }
        public tbCountry Country { get; set; }
    }
}
