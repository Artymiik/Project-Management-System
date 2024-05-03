using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pms_cs.Models;

namespace pms_cs.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    public DbSet<AppUser> AppUser { get; set; }
    public DbSet<AppCompany> AppCompany { get; set; }
    public DbSet<AppTask> AppTask { get; set; }
    public DbSet<Reports> Reports { get; set; }
}