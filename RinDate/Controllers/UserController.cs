using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RD.Models;
using RinDate.Data;
using RinDate.Models;
using RinDate.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IToolService _toolService;

        public UserController(UserManager<ApplicationUser> userManager, IToolService toolService)
        {
            _userManager = userManager;
            _toolService = toolService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var model = new UserDto()
            {
                UserId = user.Id,
                ImageCoverUrl = user.CoverImageUrl,
                Gallery = user.UserGallery.Select(img => new GalleryModel
                {
                    Id = img.Id,
                    Name = img.Name,
                    URL = img.URL
                }).ToList(),
                UserName = user.UserName
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new UserDto()
            {
                ImageCoverUrl = user.CoverImageUrl,
                Gallery = user.UserGallery.Select(img => new GalleryModel
                {
                    Id = img.Id,
                    Name = img.Name,
                    URL = img.URL
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserDto userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                string folder = "";
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                if (userModel.CoverPhoto != null)
                {
                    folder = "users/cover/";
                    user.CoverImageUrl = await _toolService.UploadImage(folder, userModel.CoverPhoto);
                }

                if (userModel.GalleryFiles != null)
                {
                    folder = "users/gallery/";

                    userModel.Gallery = new List<GalleryModel>();

                    foreach (var file in userModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await _toolService.UploadImage(folder, file)
                        };

                        userModel.Gallery.Add(gallery);
                    }

                    user.UserGallery = new List<UserGallery>();

                    foreach (var file in userModel.Gallery)
                    {
                        user.UserGallery.Add(new UserGallery()
                        {
                            Name = file.Name,
                            URL = file.URL,

                        });
                    }
                }
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
