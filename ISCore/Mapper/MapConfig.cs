using AutoMapper;
using DbCore.Entities.Users;
using ISCore.DTO.Users;


namespace ISCore.Mapper
{
    public class MapConfig:Profile
    {
        public MapConfig()
        {
            CreateMap<DTOUser, AppUser>().ReverseMap();
            CreateMap<DtoOTP, tbOTP>().ReverseMap();
        }
    }
}
