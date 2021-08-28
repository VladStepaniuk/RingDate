using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using RD.Models;
using RinDate.Data;
using RinDate.Models;
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

        public IEnumerable<UserDto> GetNearestUsers(Point myLocation)
        {
            IList<UserDto> usersFromDb = _userManager.Users
                .OrderBy(x => x.Location.Distance(myLocation))
                .Take(30)
                .Select(user => new UserDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Gallery = user.UserGallery.Select(img => new GalleryModel
                    {
                        Id = img.Id,
                        Name = img.Name,
                        URL = img.URL
                    }).ToList()
                })
                .ToList();

            //if (usersFromDb != null || usersFromDb.Count() > 0)
            //{
            //    IEnumerable<UserDto> users = usersFromDb.Select(user => new UserDto
            //    {
            //        UserId = user.Id,
            //        UserName = user.UserName,
            //        Gallery = user.UserGallery.Select(img => new GalleryModel
            //        {
            //            Id = img.Id,
            //            Name = img.Name,
            //            URL = img.URL
            //        }).ToList()

            //    }).ToList();

            //    
            //}
            //else
            //{
            //    return null;
            //}
            return usersFromDb;
        }
    }
}
