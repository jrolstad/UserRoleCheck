using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using UserRoleCheck.Models;

namespace UserRoleCheck.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var currentUser = User.Identity as WindowsIdentity;


            if (currentUser == null)
                return View(new HomeViewModel {Message = "Unable to determine your windows identity"});

            if(currentUser.Groups == null)
                return View(new HomeViewModel { Message = "You belong to no groups" });

            var groups = currentUser
                .Groups
                .Translate(typeof (NTAccount));

            var groupNames = groups
                .OrderBy(g=>g.Value)
                .Select(g => g.Value)
                .ToList();
            var viewModel = new HomeViewModel {Roles = groupNames};

            return View(viewModel);
        }
        

        
    }
}
