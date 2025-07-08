using AutoMapper;
using ISCore.DTO.Users;
using ISCore.Entities.Users;


namespace ISCore.Mapper
{
    public class MapConfig:Profile
    {
        public MapConfig()
        {
            CreateMap<DtoUser, AppUser>().ReverseMap();
            CreateMap<DtoOTP, tbOTP>().ReverseMap();
        }
    }
}
