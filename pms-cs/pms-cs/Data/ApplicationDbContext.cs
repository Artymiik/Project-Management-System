using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pms_cs.Models;

namespace pms_cs.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    public DbSet<AppUser> AppUser;
    public DbSet<AppCompany> AppCompany;
    public DbSet<AppTask> AppTask;
    public DbSet<Reports> Reports;
}