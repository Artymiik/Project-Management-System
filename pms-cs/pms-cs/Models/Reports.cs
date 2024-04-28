using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pms_cs.Models;

public class Reports
{
    [Key] public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public DateTime Beginning { get; set; }
    public DateTime Ending { get; set; }

    public string Projects { get; set; } = string.Empty;
    public string Tasks { get; set; } = string.Empty;
    
    [ForeignKey("AppUser")] public string? TypeAppUserId { get; set; }
}