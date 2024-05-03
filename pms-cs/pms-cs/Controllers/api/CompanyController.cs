using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pms_cs.Data;
using pms_cs.Models;
using pms_cs.Repository;
using pms_cs.ViewModel;

namespace pms_cs.Controllers.api;

[Authorize]
public class CompanyController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly CompanyNumber _companyNumber;

    public CompanyController(ApplicationDbContext context, UserManager<AppUser> userManager, CompanyNumber companyNumber)
    {
        _context = context;
        _userManager = userManager;
        _companyNumber = companyNumber;
        
        DotNetEnv.Env.Load();
    }
    
    public IActionResult CreateCompany()
    {
        return View();
    }
    public IActionResult SigninCompany()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateViewModel companyCM)
    {
        if (!ModelState.IsValid) return RedirectToAction("CreateCompany", "Company", companyCM);
        
        var userCurrent = await _userManager.GetUserAsync(HttpContext.User);
        if (userCurrent == null) return RedirectToAction("Login", "Account");

        var protocol = Environment.GetEnvironmentVariable("PROTOCOL");
        var domain = Environment.GetEnvironmentVariable("DOMAIN");
        var port = Environment.GetEnvironmentVariable("PORT");
        string referralLink = $"{protocol}://{domain}:{port}/Company/SigninCompany/{_companyNumber.Main()}";
        
        var company = new AppCompany()
        {
            Name = companyCM.Name,
            CompanyNumber = _companyNumber.Main(),
            ReferralLink = referralLink,
            Password = companyCM.Password,
            SecretWord = companyCM.SecretWord,
            AppUserId = userCurrent.Id,
        };

        await using (_context)
        {
            _context.AppCompany.Add(company);
            int transaction = await _context.SaveChangesAsync();

            if (transaction > 0)
            {
                var user = _context.AppUser.Find(userCurrent.Id);
                user.Company = company.CompanyNumber;
                _context.SaveChanges();
            }
        }
        
        return RedirectToAction("Index", "Home");
    }
}