using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.Entities.Places;
using ISCore.Services.DTO.Places;


namespace ISCore.Services.Places
{
    public class Srv_Countries : BaseService<tbCountry,DTOCountry,Guid>
    {
        public Srv_Countries(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
