using System.ComponentModel.DataAnnotations;

namespace pms_cs.Models;

public class AppTask
{
    [Key] public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Beginning { get; set; }
    public DateTime Ending { get; set; }
    public string AppUserIdSend { get; set; } = string.Empty;
    public string AppUserIdRecipient { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public string Status { get; set; }

    public AppTask()
    {
        var now = DateTime.Now;
        string statusDefault = "In expectation";
        
        Beginning = now;
        Status = statusDefault;
    }
}