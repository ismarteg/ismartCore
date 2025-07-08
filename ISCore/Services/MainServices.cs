using AutoMapper;
using ISCore.DataBase.Entities.Contracts;
using ISCore.Entities.Users;
using ISCore.Interfaces;
using ISCore.Services.Users;
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

      




    }
}
