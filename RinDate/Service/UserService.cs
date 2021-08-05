using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using RinDate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Service
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task UpdateCurrentUserLocation(Point location, ApplicationUser user)
        {
            user.Location = location;
            await _userManager.UpdateAsync(user);
        }
    }
}
