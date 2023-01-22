using CommandService.Models;
using PlatformCommon.Data;

namespace CommandService.Data;

public interface IPlatformRepository: IRepository<Platform, int>
{
    bool PlatformExists(int externalPlatformId);
    Task<bool> PlatformExistsAsync(int externalPlatformId);
}