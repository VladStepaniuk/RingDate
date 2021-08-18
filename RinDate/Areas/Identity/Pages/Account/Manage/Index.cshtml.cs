using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RD.Models;
using RinDate.Data;
using RinDate.Service;

namespace RinDate.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToolService _toolService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IToolService toolService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _toolService = toolService;
        }

        public string Username { get; set; }

        [Display (Name ="Choose the gallery images.")]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        [Display(Name = "Choose the cover photo of you.")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if(user.UserGallery != null)
            {
                foreach(var item in user.UserGallery)
                {
                    Gallery.Add(new GalleryModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        URL = item.URL
                    });
                }
            }

            Username = userName;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            string folder = "";
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if(CoverPhoto != null)
            {
                folder = "users/cover/";
                user.CoverImageUrl = await _toolService.UploadImage(folder, CoverPhoto);
            }

            if(GalleryFiles != null)
            {
                folder = "users/gallery/";

                Gallery = new List<GalleryModel>();

                foreach (var file in GalleryFiles)
                {
                    var gallery = new GalleryModel()
                    {
                        Name = file.FileName,
                        URL = await _toolService.UploadImage(folder, file)
                    };

                    Gallery.Add(gallery);
                }

                user.UserGallery = new List<UserGallery>();

                foreach(var file in Gallery)
                {
                    user.UserGallery.Add(new UserGallery()
                    {
                        Name = file.Name,
                        URL = file.URL,

                    });
                }
            }

            await _userManager.UpdateAsync(user);
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
