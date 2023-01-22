using System.ComponentModel.DataAnnotations;

namespace CommandService.Models;

public class Command
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public required int PlatformId { get; set; }

    [Required]
    public required string HowTo { get; set; }

    [Required]
    public required string CommandLine { get; set; }

    public virtual Platform? Platform { get; set;}
}