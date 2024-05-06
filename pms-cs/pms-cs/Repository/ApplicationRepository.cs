using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using pms_cs.Data;
using pms_cs.Interfaces;
using pms_cs.Models;
using pms_cs.ViewModel;

namespace pms_cs.Repository;

public class ApplicationRepository : AbsApplicationRepository
{
    private readonly ApplicationDbContext _ctx;
    public ApplicationRepository(ApplicationDbContext context, UserManager<AppUser> userManager) => _ctx = context;
    
    // Date Parse
    private DateTime DateParse(string endingInput)
    {
        return DateTime.ParseExact(endingInput, "dd.MM.yyyy", CultureInfo.InvariantCulture);
    }
    
    // GetFirstByCompanyNumber
    public override AppCompany GetFirstByCompanyNumber(object id)
    {
        var dataCompany = _ctx.AppCompany.FirstOrDefault(current => current.CompanyNumber == id);
        return dataCompany ?? new AppCompany();
    }

    // GetFirstByAppUserId
    public override AppCompany GetFirstByAppUserId(object id)
    {
        var data = _ctx.AppCompany.FirstOrDefault(current => current.AppUserId == id.ToString());
        return data ?? new AppCompany();
    }

    // GetAllUsersInCompany
    public override List<AppUser> GetAllUsersInCompany(string? companyId)
    {
        if (companyId == null)
            return new List<AppUser>();
        
        return _ctx.AppUser.Where(current => current.Company == companyId).ToList();
    }

    // CreateTask
    public override async Task<bool> CreateTask(TaskCreateViewModel taskViewModel, AppUser httpUser)
    {
        DateTime endingInput = DateParse(taskViewModel.Ending);

        try
        {
            var data = new AppTask()
            {
                Title = taskViewModel.Title,
                Description = taskViewModel.Description,
                Ending = endingInput,
                AppUserIdSend = httpUser.Id,
                AppUserIdRecipient = taskViewModel.UserIdRecipient,
                CompanyNumber = httpUser.Company ?? "",
            };

            await using (_ctx)
            {
                EntityEntry<AppTask> add = _ctx.AppTask.Add(data);
                await _ctx.SaveChangesAsync();
                
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}