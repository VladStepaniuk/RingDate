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
        public string ImageCoverUrl { get; set; }

        [Display]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }
    }
}
