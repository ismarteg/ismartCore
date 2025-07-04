using AutoMapper;
using DbCore.Entities.Users;
using ServicesCore.DTO.Users;


namespace ServicesCore.Mapper
{
    public class MapConfig:Profile
    {
        public MapConfig()
        {
            CreateMap<DTOUser, AppUser>().ReverseMap();
        }
    }
}
