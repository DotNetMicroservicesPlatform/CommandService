using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private ILogger<CommandsController> _logger;
    private IMapper _mapper;
    private ICommandRepository _commandRepository;
    private IPlatformRepository _platformRepository;

    public CommandsController(ILogger<CommandsController> logger, IMapper mapper, ICommandRepository commandRepository, IPlatformRepository platformRepository)
    {
        _logger = logger;
        _commandRepository = commandRepository;
        _mapper = mapper;
        _platformRepository = platformRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAsync(int platformId)
    {
        if (!await _platformRepository.PlatformExistsAsync(platformId))
        {
            _logger.LogWarning("Platform not found!");
            return NotFound();
        }

        _logger.LogInformation($"--> Getting Commands for Platform {platformId}...");

        var commandEntities = await _commandRepository.GetAllAsync(platformId);

        var commandDtos = _mapper.Map<IEnumerable<CommandReadDto>>(commandEntities);

        _logger.LogInformation("Commands returned");

        return Ok(commandDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetByIdAsync(int platformId, int id)
    {
        if (!await _platformRepository.PlatformExistsAsync(platformId))
        {
            _logger.LogWarning("Platform not found!");
            return NotFound();
        }

        _logger.LogInformation($"--> Getting Command {id} for Platform {platformId}...");

        var commandEntity = _commandRepository.GetAsync(id, platformId);

        if(commandEntity == null){
            _logger.LogWarning("Requested Command not found!");
            return NotFound();
        }

        var commandDtos = _mapper.Map<CommandReadDto>(commandEntity);

        _logger.LogInformation("Command returned");

        return Ok(commandDtos);
    }

    [HttpPost]
    public async Task<ActionResult<CommandReadDto>> PostAsync(int platformId, CommandCreateDo commandCreateDo)
    {
        if (!await _platformRepository.PlatformExistsAsync(platformId))
        {
            _logger.LogWarning("Platform not found!");
            return NotFound();
        }

        _logger.LogInformation($"--> Adding Command for Platfrom {platformId}...");

        var commadEntity = _mapper.Map<Command>(commandCreateDo);

        await _commandRepository.CreateAsync(commadEntity, platformId);

        await _commandRepository.SaveChangesAsync();

        var commandDto = _mapper.Map<CommandReadDto>(commadEntity);

        _logger.LogInformation("Command created");

        return CreatedAtAction(nameof(GetByIdAsync), new { platformId = platformId, id = commandDto.Id }, commandDto);
    }


}