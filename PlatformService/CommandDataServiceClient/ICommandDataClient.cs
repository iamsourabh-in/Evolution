using PlatformService.Dtos;
using System;
using System.Threading.Tasks;

namespace PlatformService.CommandDataServiceClient
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto platformReadDto);
    }
}