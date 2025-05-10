using AutoMapper;
using DALCore.UnitofWorks.Contracts;
using DbCore.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServicesCore.Contracts;
using ServicesCore.Email;
using ServicesCore.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCore
{
    public class MainServices:IMainServices
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;

        private Srv_Users _srv_Users;
        public MainServices(SignInManager<AppUser> signInManager, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            UserManager<AppUser> userManager, 
            IHttpContextAccessor httpContextAccessor, 
            RoleManager<AppRole> roleManager,
            IEmailSender emailSender
            )
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        public Srv_Users _Srv_Users => _Srv_Users ??
           new Srv_Users(_signInManager,_userManager,_roleManager,_emailSender,_httpContextAccessor,_unitOfWork,_mapper);

    }
}
