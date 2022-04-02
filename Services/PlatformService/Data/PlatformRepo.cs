using PlatformService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatfomRepo
    {
        private readonly PlatformAppDbContext _platformAppDbContext;
        public PlatformRepo(PlatformAppDbContext platformAppDbContext)
        {
            _platformAppDbContext = platformAppDbContext;
        }

        public async Task CreatePlatform(Platform platform)
        {
            await _platformAppDbContext.Platforms.AddAsync(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _platformAppDbContext.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _platformAppDbContext.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public Platform GetPlatformByName(string name)
        {
            return _platformAppDbContext.Platforms.FirstOrDefault(p => p.Name == name);
        }

        public bool SaveChanges()
        {
            return (_platformAppDbContext.SaveChanges() >= 0);
        }
    }
}
