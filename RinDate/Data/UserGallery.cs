using System;
using System.Collections.Generic;
using System.Text;

namespace RinDate.Data
{
    public class UserGallery
    {
        public int Id { get; set; } 
        public string ApplicationUserId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
