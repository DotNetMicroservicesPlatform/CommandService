using AutoMapper;
using CommandService.Data;
using CommandService.Models;
using MassTransit;
using PlatformContracts.Dtos;

namespace CommandService.Consumers;

public class PlatformCreatedConsumer : IConsumer<PlatformCreated>
{
    private readonly ILogger<PlatformCreatedConsumer> _logger;
    private readonly IMapper _mapper;

    private readonly IPlatformRepository _platformRepo;

    public PlatformCreatedConsumer(ILogger<PlatformCreatedConsumer> logger, IPlatformRepository platformRepository, IMapper mapper)
    {
        _logger = logger;
        _platformRepo = platformRepository;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<PlatformCreated> context)
    {
        _logger.LogInformation("-> Received Platform Created Message...");

        var message = context.Message;

        var exists = await _platformRepo.PlatformExistsAsync(message.Id);
        if (exists)
        {
            _logger.LogInformation("Platform already exists");
            return;
        }
        
        _logger.LogInformation("--> Adding Platform...");

        var platformModel = _mapper.Map<Platform>(message);

        await _platformRepo.CreateAsync(platformModel);

        await _platformRepo.SaveChangesAsync();

        _logger.LogInformation("Platform created");
    }
}