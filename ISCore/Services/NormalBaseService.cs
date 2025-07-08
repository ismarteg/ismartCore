using AutoMapper;
using ISCore.Interfaces;
using ISCore.Responses;


namespace Services
{
    public class NormalBaseService<TEntity> where TEntity : class 
    {
        protected IUnitOfWork _UnitOfWork;
        protected IMapper _Mapper;
        protected IRepository<TEntity> _TbRepository;
        protected SrvResponse _response;
        public NormalBaseService()
        {
            _response = new SrvResponse();
        }
        public NormalBaseService(IUnitOfWork unitOfWork, IMapper _mapper)
        {
            _response = new SrvResponse();
            _UnitOfWork = unitOfWork;
            _Mapper = _mapper;
            _TbRepository = _UnitOfWork.repo<TEntity>();
        }

    }
}
