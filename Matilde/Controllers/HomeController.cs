using System;
using System.Diagnostics;
using System.Web;
using KK.AspNetCore.EasyAuthAuthentication;
using Microsoft.AspNetCore.Mvc;
using Matilde.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Matilde.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var n = HttpContext.User.Identity.Name;
            ViewData["Title"] = "Ma~ (Matilda) is an artificial intelligence.";
            ViewData["Message"] = "Ask her a health or wellness question.";
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
