﻿using ISCore.Entities.Places;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Entities.Places
{
    [Table(nameof(tbCountry), Schema = "main")]
    public class tbCountry : GuidBaseEntity
    {
        public string Title { get; set; }
        
    }
}
