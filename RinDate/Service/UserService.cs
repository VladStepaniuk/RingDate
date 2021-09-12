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
        private ApplicationDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task UpdateCurrentUserLocation(Point location, ApplicationUser user)
        {
            user.Location = location;
            await _userManager.UpdateAsync(user);
        }

        public async Task<UserDto> GetUserById(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            IEnumerable<GalleryModel> galleries = _context.UserGalleries.Where(x => x.ApplicationUserId == id)
                .Select(gal => new GalleryModel
                {
                    Id = gal.Id,
                    Name = gal.Name,
                    URL = gal.URL
                }).ToList();

            if(user != null)
            {
                UserDto userDto = null;

                if (galleries.Count() > 0)
                {
                    userDto = new UserDto
                    {
                        UserId = user.Id,
                        ImageCoverUrl = user.CoverImageUrl,
                        Age = user.Age,
                        Description = user.AboutMe,
                        Gallery = galleries.ToList(),
                        UserName = user.UserName
                    };
                }
                else
                {
                    userDto = new UserDto
                    {
                        UserId = user.Id,
                        ImageCoverUrl = user.CoverImageUrl,
                        Age = user.Age,
                        Description = user.AboutMe,
                        UserName = user.UserName
                    };
                }

                return userDto;
            }
            else return null;
        }

        public async Task UpdateUserProfile(UserDto userModel, ApplicationUser user)
        {
            user.AboutMe = userModel.Description;
            user.Age = userModel.Age;
            user.UserName = userModel.UserName;

            user.UserGallery = new List<UserGallery>();

            if (userModel.Gallery != null)
            {
                foreach (var file in userModel.Gallery)
                {
                    user.UserGallery.Add(new UserGallery()
                    {
                        Name = file.Name,
                        URL = file.URL,
                        ApplicationUserId = user.Id,
                        ApplicationUser = user
                    });
                }
            }

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
