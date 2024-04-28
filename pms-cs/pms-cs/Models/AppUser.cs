using Microsoft.AspNetCore.Identity;

namespace pms_cs.Models;

public class AppUser : IdentityUser
{
    public string? Company { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<AppCompany> AppCompany { get; set; }
    public ICollection<Reports> Reports { get; set; }
    public ICollection<AppTask> AppTask { get; set; }

    
    public AppUser()
    {
        var now = DateTime.Now;
        CreatedAt = now;
    }
}