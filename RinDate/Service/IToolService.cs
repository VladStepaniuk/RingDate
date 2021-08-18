using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Service
{
    public interface IToolService
    {
        Task<string> UploadImage(string folderPath, IFormFile file);
    }
}
