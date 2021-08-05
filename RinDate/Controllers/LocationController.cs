using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using RinDate.Data;
using RinDate.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Controllers
{
    public class LocationController : Controller
    {
        private IUserService _userService;
        private ILocationService _locationService;
        private UserManager<ApplicationUser> _userManager;

        public LocationController(IUserService userService, UserManager<ApplicationUser> userManager, ILocationService locationService)
        {
            _userService = userService;
            _userManager = userManager;
            _locationService=locationService;
        }

        public IActionResult Desk()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNearUsers(float latitude, float longitude)
        {
            Point location = new Point(latitude, longitude)
            {
                SRID = 4326
            };

            var user = await _userManager.GetUserAsync(User);
            await _userService.UpdateCurrentUserLocation(location, user);
            IList<ApplicationUser> nearbyUsers = _locationService.GetNearestUsers(user.Location);

            return PartialView("_Users", nearbyUsers);
        }
    }
}
