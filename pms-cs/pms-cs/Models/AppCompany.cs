using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pms_cs.Models;

public class AppCompany
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CompanyNumber { get; set; } = string.Empty;
    public string ReferralLink { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string SecretWord { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }

    public AppCompany()
    {
        var now = DateTime.Now;
        CreatedAt = now;
    }
}