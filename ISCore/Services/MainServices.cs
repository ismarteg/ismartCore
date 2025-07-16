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
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailSender;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IHttpContextAccessor _httpContextAccessor;

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
