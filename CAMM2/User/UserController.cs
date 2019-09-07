// Generated Code! Do not manually edit. Adjust template (ControllersGenerator.tt) to make changes to this file.
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Application.Service;

namespace Presentation
{ 
    public class UserController : UserBaseController
    {              
        private readonly IUserService _userService;        

        public UserController(IUserService userService) : base(userService)
        { 
            _userService = userService;            
        }

    }
}
