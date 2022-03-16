using PlatformService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public interface IPlatfomRepo
    {

        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();

        Platform GetPlatformById(int id);
        Platform GetPlatformByName(string name);

        Task CreatePlatform(Platform platform);
    }
}
