using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.Entities.Places;
using ISCore.Services.DTO.Places;


namespace ISCore.Services.Places
{
    public class Srv_Regions : BaseService<tbRegion,DTORegion,Guid>
    {
        public Srv_Regions(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
