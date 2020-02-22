using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hwnlp2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace hwnlp2.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var n = HttpContext.User.Identity.Name;
            ViewData["Title"] = "> Search topics";
            ViewData["Message1"] = "for the best matching";
            ViewData["Message2"] = "inferred concept and aspect,";
            ViewData["Message3"] = "return the first sentence";
            ViewData["UserIdent"] = n;
            //UserStuff();
            return View();
        }


        private void UserStuff()
        {
            var mulps = User.HasClaim(ClaimTypes.Name, "user@somecloud.onmicrosoft.com");
            var peng = User.HasClaim(ClaimTypes.Role, "SystemAdmin");
            // var blubb = HttpContext.User.IsInRole("SystemAdmin");
            //var pop = User.IsInRole("SystemAdmin");
            var n = HttpContext.User.Identity.Name;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
