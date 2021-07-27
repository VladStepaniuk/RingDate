using Microsoft.AspNetCore.Identity;
using RD.Models;
using RD.Models.Sections;
using RD.Models.Sections.ExpectSect;
using RD.Models.Sections.IdenSect;
using RD.Models.Sections.StatSect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.FavouriteUsers = new HashSet<ApplicationUser>();
        }

        public string ShowedName { get; set; }
        public string AboutMe { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<ExpItems> Expectations { get; set; }
        public IEnumerable<MeetItems> Meets { get; set; }
        public bool TakePictures { get; set; }
      
        public int Age { get; set; }
        public bool ShowAge { get; set; }
        public int Hight { get; set; }
        public int Weight { get; set; }
        public HashSet<ApplicationUser> FavouriteUsers { get; set; }
        
    }
}
