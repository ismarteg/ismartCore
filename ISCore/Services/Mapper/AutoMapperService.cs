using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Services.Mapper
{
    public static class AutoMapperService
    {
        public static IMapper Mapper { get; private set; }

        public static void Initialize(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
