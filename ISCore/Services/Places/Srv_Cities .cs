using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.Entities.Places;
using ISCore.Services.DTO.Places;
using ISCore.Services.Mapper;
using Microsoft.EntityFrameworkCore;


namespace ISCore.Services.Places
{
    public class Srv_Cities : BaseService<tbCity,DTOCity,Guid>
    {
        public Srv_Cities(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public SrvResponse GetPage(int pageIndex, int pageSize, string? search = null, bool isAsc = true,Guid? CountryId = null)
        {
            List<tbCity> list = _TbRepository.GetPage(pageIndex, pageSize, out int Count,
                predicate: x => (search == null || x.Title.Contains(search)) && (CountryId == null || x.CountryId == CountryId),
                includes: x=> x.Include(x=>x.Country),
                orderBy: x => isAsc ? x.OrderBy(y => y.Title).ThenBy(y => y.Id) : x.OrderByDescending(y => y.Title).ThenBy(y => y.Id)
                ).ToList();
            return _response.Success(list.MapList<DTOCity>(), Count);
        }
        public SrvResponse GetCities(Guid CountryId)
        {
            List<tbCity> list = _TbRepository.GetAll(
              predicate: x =>  x.CountryId == CountryId,
              orderBy: x => x.OrderBy(y => y.Title).ThenBy(y => y.Id) 
              ).ToList();
            return _response.Success(list.MapList<DTOCity>());

        }
    }
}
