using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.Entities.Places;
using ISCore.Services.DTO.Places;
using ISCore.Services.Mapper;


namespace ISCore.Services.Places
{
    public class Srv_Countries : BaseService<tbCountry,DTOCountry,Guid>
    {
        public Srv_Countries(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public SrvResponse GetPage(int pageIndex, int pageSize, string? search = null, bool isAsc = true)
        {
            List<tbCountry> list = _TbRepository.GetPage(pageIndex, pageSize, out int Count,
                predicate: x => (search == null || x.Title.Contains(search))
                ,orderBy: x => isAsc ? x.OrderBy(y => y.Title).ThenBy(y=>y.Id) : x.OrderByDescending(y => y.Title).ThenBy(y => y.Id)
                ).ToList();
            return _response.Success(list.MapList<DTOCountry>(), Count);
        }
    }
}
