using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.Entities.Places;
using ISCore.Services.DTO.Places;
using ISCore.Services.Mapper;
using Microsoft.EntityFrameworkCore;


namespace ISCore.Services.Places
{
    public class Srv_Regions : BaseService<tbRegion,DTORegion,Guid>
    {
        public Srv_Regions(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public SrvResponse GetPage(int pageIndex, int pageSize, string? search = null, bool isAsc = true,Guid? CityId=null)
        {
            List<tbRegion> list = _TbRepository.GetPage(pageIndex, pageSize, out int Count,
                predicate: x => x.IsDeleted==false && (search == null || x.Title.Contains(search))&&(CityId == null || x.CityId == CityId),
                includes: x => x.Include(y => y.City), 
                orderBy: x => isAsc ? x.OrderBy(y => y.Title).ThenBy(y => y.Id) : x.OrderByDescending(y => y.Title).ThenBy(y => y.Id)
                ).ToList();
            return _response.Success(list.MapList<DTORegion>(), Count);
        }
        public SrvResponse GetRegion(Guid CityId)
        {
            List<tbRegion> list = _TbRepository.GetAll(
              predicate: x => x.CityId == CityId,
              orderBy: x => x.OrderBy(y => y.Title).ThenBy(y => y.Id)
              ).ToList();
            return _response.Success(list.MapList<DTORegion>());
        }
        public SrvResponse GetDeapItem(Guid Id) {
            tbRegion item = _TbRepository.GetFirstOrDefault(predicate: x => x.Id == Id,
                includes: x => x.Include(y => y.City).ThenInclude(x => x.Country)
                );
            return _response.Success(item.MapItem<DTORegion>());
        }
    }
}
