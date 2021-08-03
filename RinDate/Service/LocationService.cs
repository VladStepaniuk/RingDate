using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using RinDate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Service
{
    public class LocationService : ILocationService
    {
        private UserManager<ApplicationUser> _userManager;

        public LocationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<ApplicationUser> GetNearestUsers(Point myLocation)
        {
            IList<ApplicationUser> users = _userManager.Users
                .OrderBy(x => x.Location.Distance(myLocation))
                .Take(30)
                .ToList();

            return users;
        }
    }
}
