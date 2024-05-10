using System.Diagnostics.Tracing;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using pms_cs.Data;
using pms_cs.Interfaces;
using pms_cs.Models;
using pms_cs.ViewModel;

namespace pms_cs.Repository;

public class ApplicationRepository : AbsApplicationRepository
{
    private readonly ApplicationDbContext _ctx;
    public ApplicationRepository(ApplicationDbContext context) => _ctx = context;
    
    // Date Parse
    private DateTime DateParse(string endingInput)
    {
        return DateTime.ParseExact(endingInput, "dd.MM.yyyy", CultureInfo.InvariantCulture);
    }
    
    // GetFirstByCompanyNumber
    public override AppCompany GetFirstByCompanyNumber(string id)
    {
        var dataCompany = _ctx.AppCompany.FirstOrDefault(current => current.CompanyNumber == id);
        return dataCompany ?? new AppCompany();
    }

    // GetFirstByAppUserId
    public override AppCompany GetFirstByAppUserId(string id)
    {
        var data = _ctx.AppCompany.FirstOrDefault(current => current.AppUserId == id);
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
        try
        {
            var endingInput = DateParse(taskViewModel?.Ending);
        
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
                _ctx.AppTask.Add(data);
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

    // Get all task (Index: page)
    public override IQueryable<AppTask> GetAllTasks(string id)
    {
        var data = _ctx.AppTask.Where(task => task.AppUserIdRecipient == id);
        return data;
    }

    // All info task for (Details: page)
    public override AppTask? GetInfoAppTaskById(int id)
    {
        return _ctx.AppTask.SingleOrDefault(task => task.Id == id);
    }

    public override string GetUsernameSending(string id)
    {
        return _ctx.AppUser.FirstOrDefault(user => user.Id == id)?.UserName ?? id;
    }
    public override string GetCompanyName(string id)
    {
        return _ctx.AppCompany.FirstOrDefault(company => company.CompanyNumber == id)?.Name ?? id;
    }
}