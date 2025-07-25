using AutoMapper;
using ISCore.DAL.interfaces;
using ISCore.DataBase.Entities.Contracts;
using ISCore.Entities.Users;
using ISCore.Services.Interface;
using ISCore.Services.Places;
using ISCore.Services.Users;
using ISCore.Utils.Emails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Services
{
    public class MainServices: IMainServices
    {
     
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IEmailServices _emailSender;
        protected readonly IHostingEnvironment _hostingEnv;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        Srv_Countries _srvCountries;
        Srv_Cities _srvCities;
        Srv_Regions _srvRegions;

        public MainServices( 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor,    
            IEmailServices emailSender,
            IHostingEnvironment hostingEnv
            )
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _hostingEnv = hostingEnv;
        }


       public  Srv_Countries _SrvCountries => _srvCountries?? new Srv_Countries(_unitOfWork, _mapper);
        public Srv_Cities _SrvCities => _srvCities?? new Srv_Cities(_unitOfWork, _mapper);
        public Srv_Regions _SrvRegions => _srvRegions?? new Srv_Regions(_unitOfWork, _mapper);



    }
}
