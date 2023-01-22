using CommandService.Models;
using PlatformCommon.Data;

namespace CommandService.Data;

public interface ICommandRepository: IRepository<Command, int>
{   

    IEnumerable<Command> GetAll(int platformId);
    Task<IEnumerable<Command>> GetAllAsync(int platformId);

    Command? Get(int id, int platformId);
    Task<Command?> GetAsync(int id, int platformId);

    void Create(Command command, int platformId);
    Task CreateAsync(Command command, int platformId);
}