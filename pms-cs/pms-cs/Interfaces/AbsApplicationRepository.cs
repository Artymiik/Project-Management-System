using pms_cs.Models;
using pms_cs.ViewModel;

namespace pms_cs.Interfaces;

public abstract class AbsApplicationRepository
{
    public abstract AppCompany GetFirstByCompanyNumber(string id);
    public abstract AppCompany GetFirstByAppUserId(string id);
    public abstract List<AppUser> GetAllUsersInCompany(string companyId);
    public abstract Task<bool> CreateTask(TaskCreateViewModel taskViewModel, AppUser httpUser);
    public abstract IQueryable<AppTask> GetAllTasks(string id);
    
    // Details
    public abstract AppTask? GetInfoAppTaskById(int id);
    public abstract string GetUsernameSending(string id);
    public abstract string GetCompanyName(string id);
}