using RinDate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Service
{
    public interface IUserService
    {
        Task<IList<ApplicationUser>> GetUserList();
    }
}
