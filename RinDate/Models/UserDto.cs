using Microsoft.AspNetCore.Http;
using RD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Models
{
    public class UserDto
    {
        [Display(Name = "Choose the cover photo for profile.")]
        public IFormFile CoverPhoto { get; set; }

        [Display(Name = "Cover image")]
        public string ImageCoverUrl { get; set; }

        [Display(Name = "Gallery files")]
        public IFormFileCollection GalleryFiles { get; set; }
        [Display(Name = "Gallery")]
        public List<GalleryModel> Gallery { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
    }
}
