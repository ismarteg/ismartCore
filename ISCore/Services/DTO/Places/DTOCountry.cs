using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISCore.Services.DTO;

namespace ISCore.Services.DTO.Places
{
    public class DTOCountry : DTOBase<Guid>
    {
        public string Title { get; set; }
    }
}
