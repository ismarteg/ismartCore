using AutoMapper;
using ISCore.Entities.Contracts;
using ISCore.Interfaces;
using ISCore.Responses;
using ISCore.Mapper;
using ISCore.Utils;

namespace ISCore.Services
{
    public class BaseService<TEntity, TDto,Tkey>
      where TEntity : class, iSoftEntity<Tkey>, new()
    where TDto : class
    {
        protected readonly IUnitOfWork _UnitOfWork;
        protected readonly IMapper _Mapper;
        protected readonly IRepository<TEntity> _TbRepository;
        protected readonly SrvResponse _response;

        public BaseService()
        {
            _response = new SrvResponse();
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            _TbRepository = _UnitOfWork.repo<TEntity>();
            _response = new SrvResponse();
        }

        public virtual SrvResponse GetItem(Guid id)
        {
            var entity = _TbRepository.Get(id);
            if (entity == null)
                return _response.Error("Item not found.");

            var dto = entity.MapItem<TDto>(); // or _mapper.Map<TDto>(entity);
            return _response.Success(dto);
        }

        public virtual SrvResponse DeleteItem(Guid id, string userId)
        {
            var entity = _TbRepository.Get(id);
            if (entity == null)
                return _response.Error("Item not found.");

            entity.SetDeleteData(userId); // يستخدم الـ Extension method بتاعتك
            _TbRepository.Update(entity);

            var result = _UnitOfWork.Save();
            return result.IsCompleted
                ? _response.Success()
                : _response.Error("Error occurred while deleting the item. " + result.Message);
        }

        public virtual SrvResponse SaveItem(TDto dto, string userId)
        {
            TEntity entity = dto.MapItem<TEntity>(); // or _mapper.Map<TEntity>(dto);
            if (entity == null)
                return _response.Error("Mapping failed. Please check the input data.");

            dto.GetIDProperty(out Guid? id);

            if (id == null || id == Guid.Empty)
            {
                entity.SetIDProperty(Guid.NewGuid());
                entity.SetCreationData(userId);
                _TbRepository.Add(entity);
            }
            else
            {
                //var existing = _TbRepository.Get(id.Value);
                //if (existing == null)
                //    return _response.Error("Item not found for update.");

                entity.SetUpdateData(userId);
                _TbRepository.Update(entity);
            }
            var result = _UnitOfWork.Save();
            return result.IsCompleted
                ? _response.Success()
                : _response.Error("Error occurred while saving the item. " + result.Message);
        }
        public virtual SrvResponse GetAll()
        {
            try
            {
                var data = _TbRepository.GetAll(x => x.IsDeleted == false).ToList();
                var result = data.MapList<TDto>();
                return _response.Success(result);
            }
            catch (Exception ex)
            {
                return _response.Error("An error occurred while retrieving all items: " + ex.Message);
            }
        }
        public SrvResponse GetList(int PageIndex = 1, int PageSize = 50)
        {
            try
            {
                var data = _TbRepository.GetPage(PageIndex, PageSize, out int totalCount, x => x.IsDeleted==false).ToList();
                var result = data.MapList<TDto>();
                return _response.Success(result);
            }
            catch (Exception ex)
            {
                return _response.Error("An error occurred while retrieving the list: " + ex.Message);
            }
        }
     
    }

}
