using System;

namespace RD.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public bool AvaImage { get; set; }
    }
}
