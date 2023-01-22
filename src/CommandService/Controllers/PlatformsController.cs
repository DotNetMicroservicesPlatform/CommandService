using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly ILogger<PlatformsController> _logger;
    private readonly IPlatformRepository _platformRepository;
    private readonly IMapper _mapper;

    public PlatformsController(ILogger<PlatformsController> logger, IPlatformRepository platformRepository, IMapper mapper)
    {
        _logger = logger;
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAsync(){

        _logger.LogInformation("--> Getting Platforms...");

        var platformModels=await _platformRepository.GetAllAsync();

        var platformDtos=_mapper.Map<IEnumerable<PlatformReadDto>>(platformModels);

        _logger.LogInformation("Platforms returned");

        return Ok(platformDtos);
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        _logger.LogInformation("--> Inbound Post # Command Service");

        return Ok("Inbound test of from platforms controller");
    }
}