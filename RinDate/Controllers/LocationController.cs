using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Desk()
        {
            return View();
        }
    }
}
