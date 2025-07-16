using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.Entities.Places;
using ISCore.Services.DTO.Places;


namespace ISCore.Services.Places
{
    public class Srv_Cities : BaseService<tbCity,DTOCity,Guid>
    {
        public Srv_Cities(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
