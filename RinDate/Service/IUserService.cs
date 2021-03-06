using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;
using RinDate.Data;
using RinDate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Service
{
    public interface IUserService
    {
        Task UpdateCurrentUserLocation(Point location, ApplicationUser user);
        Task<UserDto> GetUserById(string id);

        Task UpdateUserProfile(UserDto userModel, ApplicationUser user);
   
    }
}
