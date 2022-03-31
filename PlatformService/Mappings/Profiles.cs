using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}
