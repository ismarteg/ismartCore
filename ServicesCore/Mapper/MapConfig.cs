using AutoMapper;
using DbCore.Entities.Clients;
using DbCore.Entities.Users;
using ServicesCore.TDO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCore.Mapper
{
    public class MapConfig:Profile
    {
        public MapConfig()
        {
            CreateMap<TDOUser, AppUser>().ReverseMap();
            CreateMap<TDOClient, tbClient>()
                  .ForMember(x => x.CreationDate, opt => opt.Ignore())
                 .ForMember(x => x.CreatorId, opt => opt.Ignore())
                 .ForMember(x => x.LastEditDate, opt => opt.Ignore())
                 .ForMember(x => x.LastEditorId, opt => opt.Ignore()).ReverseMap();
        }
    }
}
