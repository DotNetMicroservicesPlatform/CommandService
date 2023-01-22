using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data;

public class CommandRepository : ICommandRepository
{

    private readonly AppDbContext _context;

    public CommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(Command command)
    {
        ArgumentNullException.ThrowIfNull(command);
        _context.Commands.Add(command);
    }

    public async Task CreateAsync(Command command)
    {
        ArgumentNullException.ThrowIfNull(command);
        await _context.Commands.AddAsync(command);
    }

    public Command? Get(int id)
    {
        return _context.Commands.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Command?> GetAsync(int id)
    {
        return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
    }

    public Command? Get(int id, int platformId)
    {
        return _context.Commands.FirstOrDefault(c => c.Id == id && c.PlatformId == platformId);
    }

    public async Task<Command?> GetAsync(int id, int platformId)
    {
        return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id && c.PlatformId == platformId);
    }

    public IEnumerable<Command> GetAll()
    {
        return _context.Commands.ToList();
    }

    public async Task<IEnumerable<Command>> GetAllAsync()
    {
        return await _context.Commands.ToListAsync();
    }

    public IEnumerable<Command> GetAll(int platformId)
    {
        return _context.Commands.Where(c => c.PlatformId == platformId).ToList();
    }

    public async Task<IEnumerable<Command>> GetAllAsync(int platformId)
    {
        return await _context.Commands.Where(c => c.PlatformId == platformId).ToListAsync();
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }


    public void Create(Command command, int platformId)
    {
        ArgumentNullException.ThrowIfNull(command);
        command.PlatformId = platformId;
        Create(command);
    }

    public async Task CreateAsync(Command command, int platformId)
    {
        ArgumentNullException.ThrowIfNull(command);
        command.PlatformId=platformId;
        await CreateAsync(command);
    }
    
}
