using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pms_cs.Data;
using pms_cs.Models;

namespace pms_cs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Find id AppUser
        var userCurrent = await _userManager.GetUserAsync(HttpContext.User);
        if (userCurrent == null) return RedirectToAction("Login", "Account");

        // Return all data company
        var com = _context.AppCompany.Where(current => current.AppUserId == userCurrent.Id).FirstOrDefault();

        // Return all users for company
        var company = _context.AppUser.Where(current => current.Company == userCurrent.Company
                                        && current.Company != null);
        List<AppUser> users = company.ToList();
        
        // create IEnumerable users for AppTask
        var usersTask = _context.AppUser.Where(current => current.Company == userCurrent.Company)
                                            .Select(username => username.UserName).ToList();

        var indexModel = new IndexModel()
        {
            AppUser = users,
            UsersTask = usersTask,
            AppCompany = com,
        };
        
        return View(indexModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}