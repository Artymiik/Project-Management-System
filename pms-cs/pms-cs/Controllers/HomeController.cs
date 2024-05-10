using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pms_cs.Data;
using pms_cs.Interfaces;
using pms_cs.Models;
using pms_cs.ViewModel;

namespace pms_cs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly AbsApplicationRepository _absApplicationRepository;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, AbsApplicationRepository absApplicationRepository, ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
        _absApplicationRepository = absApplicationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Find id AppUser
        var userCurrent = await _userManager.GetUserAsync(HttpContext.User);
        if (userCurrent == null) return RedirectToAction("Login", "Account");

        // Return all data company
        var com = _absApplicationRepository.GetFirstByAppUserId(userCurrent.Id);

        // Return all users for company
        var company = _context.AppUser.Where(current => current.Company == userCurrent.Company
                                        && current.Company != null);
        List<AppUser> users = company.ToList();
        
        // create IEnumerable users for AppTask
        List<AppUser> usersTask = _absApplicationRepository.GetAllUsersInCompany(userCurrent.Company);
        
        // Get all task
        List<AppTask> tasks = _absApplicationRepository.GetAllTasks(userCurrent.Id).ToList();

        var indexModel = new IndexModel()
        {
            AppUser = users,
            UsersTask = usersTask,
            AppCompany = com,
            AllTasks = tasks,
            TaskCreateViewModel = new TaskCreateViewModel(), 
        };
        
        return View(indexModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}