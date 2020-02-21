using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Matilde.Controllers
{
    public class ClassyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}