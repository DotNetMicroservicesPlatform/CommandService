using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos;

public class CommandCreateDo
{
    [Required]
    public required string HowTo { get; set; }

    [Required]
    public required string CommandLine { get; set; }
}