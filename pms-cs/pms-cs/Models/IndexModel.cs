namespace pms_cs.Models;

public class IndexModel
{
    public IEnumerable<AppUser>? AppUser { get; set; }
    public List<string?> UsersTask { get; set; }
    public AppCompany? AppCompany { get; set; }
}