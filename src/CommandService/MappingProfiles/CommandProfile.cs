using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using PlatformContracts.Dtos;
using PlatformService;

namespace CommandService.MappingProfiles;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        // Source -> Destination
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDo, Command>();
        CreateMap<PlatformCreated, Platform>()
                .ForMember(dest => dest.ExternalPlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalPlatformId, opt => opt.MapFrom(src => src.PlatformId));
    }
}