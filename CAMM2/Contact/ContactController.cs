 
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
    public class ContactController : ContactBaseController
    {              
        private readonly IContactService _contactService;        

        public ContactController(IContactService contactService) : base(contactService)
        { 
            _contactService = contactService;            
        }

    }
}
