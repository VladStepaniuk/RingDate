using NetTopologySuite.Geometries;
using RinDate.Data;
using RinDate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinDate.Service
{
    public interface ILocationService
    {
        IEnumerable<UserDto> GetNearestUsers(Point myLocation);
    }
}
