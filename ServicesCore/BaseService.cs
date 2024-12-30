using AutoMapper;
using DALCore.Repositories.Contracts;
using DALCore.UnitofWorks.Contracts;
using ServicesCore.Utilities;

namespace ServicesCore
{
    public class BaseService<TEntity> where TEntity : class
    {
        protected IUnitOfWork _UnitOfWork;
        protected IMapper _Mapper;
        protected IRepository<TEntity> _TbRepository;
        protected SrvResponse _response;
        public BaseService()
        {
            _response = new SrvResponse();
        }
        public BaseService(IUnitOfWork unitOfWork, IMapper _mapper)
        {
            _response = new SrvResponse();
            _UnitOfWork = unitOfWork;
            _Mapper = _mapper;
            _TbRepository = _UnitOfWork.repo<TEntity>();
        }

    }
}
