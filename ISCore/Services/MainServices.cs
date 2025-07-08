using AutoMapper;
using ISCore.Entities.Users;
using ISCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Services
{
    public class MainServices:IMainServices
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailSender;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        /// <summary>
        /// private Srv_Users _srv_Users;
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="roleManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="hostingEnv"></param>

        public MainServices(SignInManager<AppUser> signInManager, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            UserManager<AppUser> userManager, 
            IHttpContextAccessor httpContextAccessor, 
            RoleManager<AppRole> roleManager,
            IEmailServices emailSender,
            IHostingEnvironment hostingEnv
            )
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _hostingEnv = hostingEnv;
        }

        //public Srv_Users _Srv_Users => _srv_Users ??
        //   new Srv_Users(_signInManager,_userManager,_roleManager,_emailSender,_httpContextAccessor,_unitOfWork,_mapper);

      

      

    }
}
