using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using RD.Models;
using RinDate.Data;
using RinDate.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<UserDto> GetUserById(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                UserDto userDto = new UserDto
                {
                    UserId = user.Id,
                    ImageCoverUrl = user.CoverImageUrl,
                    Age = user.Age,
                    Description = user.AboutMe,
                    //Gallery = user.UserGallery.Select(img => new GalleryModel
                    //{
                    //    Id = img.Id,
                    //    Name = img.Name,
                    //    URL = img.URL
                    //}).ToList(),
                    UserName = user.UserName
                };

                return userDto;
            }
            else return null;
        }
    }
}
