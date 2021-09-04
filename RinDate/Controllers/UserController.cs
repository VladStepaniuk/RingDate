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
        private ApplicationDbContext _context;
        private IToolService _toolService;
        private IUserService _userService;

        public UserController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IToolService toolService,
            IUserService userService)
        {
            _userManager = userManager;
            _context = context;
            _toolService = toolService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            UserDto model = await _userService.GetUserById(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(string id)
        {
            return View(await _userService.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserDto userModel)
        {
            ApplicationUser user = null;

            if (ModelState.IsValid)
            {
                user = await _userManager.GetUserAsync(User);
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
                user.AboutMe = userModel.Description;
                user.Age = userModel.Age;
                user.UserName = userModel.UserName;

                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", user.Id);
        }
    }
}
