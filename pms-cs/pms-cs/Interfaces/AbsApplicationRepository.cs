using pms_cs.Models;
using pms_cs.ViewModel;

namespace pms_cs.Interfaces;

public abstract class AbsApplicationRepository
{
    public abstract AppCompany GetFirstByCompanyNumber(object id);
    public abstract AppCompany GetFirstByAppUserId(object id);
    public abstract List<AppUser> GetAllUsersInCompany(string companyId);
    public abstract Task<bool> CreateTask(TaskCreateViewModel taskViewModel, AppUser httpUser);
}