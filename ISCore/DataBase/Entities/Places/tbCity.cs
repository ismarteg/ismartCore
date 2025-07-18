﻿using ISCore.Entities.Places;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Entities.Places
{
    [Table(nameof(tbCity), Schema = "main")]
    public class tbCity: GuidBaseEntity
    {
        public string Title { get; set; }

        [ForeignKey(nameof(tbCountry))]
        public Guid CountryId { get; set; }
        public tbCountry Country { get; set; }
    }
}
