using AutoMapper;
using ISCore.Entities.Places;
using ISCore.Entities.Users;
using ISCore.Services.DTO.Places;
using ISCore.Services.DTO.Users;


namespace ISCore.Services.Mapper
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<DtoUser, AppUser>().ReverseMap();
            CreateMap<DtoOTP, tbOTP>().ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreatorId, opt => opt.Ignore())
                .ForMember(x => x.LastEditDate, opt => opt.Ignore())
                .ForMember(x => x.LastEditorId, opt => opt.Ignore()).ReverseMap();
            CreateMap<DTOCountry, tbOTP>().ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreatorId, opt => opt.Ignore())
                .ForMember(x => x.LastEditDate, opt => opt.Ignore())
                .ForMember(x => x.LastEditorId, opt => opt.Ignore()).ReverseMap(); ;
            CreateMap<DTOCity, tbCity>().ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreatorId, opt => opt.Ignore())
                .ForMember(x => x.LastEditDate, opt => opt.Ignore())
                .ForMember(x => x.LastEditorId, opt => opt.Ignore()).ReverseMap(); ;
            CreateMap<DTOCity, tbRegion>().ForMember(x => x.CreationDate, opt => opt.Ignore())
                .ForMember(x => x.CreatorId, opt => opt.Ignore())
                .ForMember(x => x.LastEditDate, opt => opt.Ignore())
                .ForMember(x => x.LastEditorId, opt => opt.Ignore()).ReverseMap();
        }
    }
}
