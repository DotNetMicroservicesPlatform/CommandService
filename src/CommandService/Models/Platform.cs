using System.ComponentModel.DataAnnotations;

namespace CommandService.Models;

public class Platform
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public required int ExternalPlatformId { get; set; }

    [Required]
    public required string Name { get; set; }

    public virtual ICollection<Command> Commands { get; set; } = new List<Command>();
}