using AutoMapper;
using CommandService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandService.SyncDataServices.Grpc;

public class PlatformDataClient : IPlatformDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    private readonly ILogger<PlatformDataClient> _logger;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper, ILogger<PlatformDataClient> logger)
    {
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<Platform>?> ReturnAllPlatformsAsync()
    {
        _logger.LogInformation($"--> Calling Platforms GRPC Service {_configuration["PlatformGrpcUrl"]}...");
        var channel = GrpcChannel.ForAddress(_configuration["PlatformGrpcUrl"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();
        try
        {
            var response= await client.GetAllPlatformsAsync(request);
            var platforms= _mapper.Map<IEnumerable<Platform>>(response.Platforms);
            return platforms; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "--> Could not call GRPC Server!");
            return null;
        }
    }

    public IEnumerable<Platform>? ReturnAllPlatforms()
    {
        _logger.LogInformation($"--> Calling Platforms GRPC Service {_configuration["PlatformGrpcUrl"]}...");
        var channel = GrpcChannel.ForAddress(_configuration["PlatformGrpcUrl"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();
        try
        {
            var response= client.GetAllPlatforms(request);
            var platforms= _mapper.Map<IEnumerable<Platform>>(response.Platforms);
            return platforms; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "--> Could not call GRPC Server!");
            return null;
        }
    }
}