using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pms_cs.Data;
using pms_cs.Models;
using pms_cs.Repository;
using pms_cs.ViewModel;

namespace pms_cs.Controllers.api;

[Authorize]
public class CompanyController : Controller
{
    // private
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
    public async Task<IActionResult> SigninCompany(string id)
    {
        Console.WriteLine($"id: {id}");
        var checkingCompany = await _context.AppCompany.FirstOrDefaultAsync(current => current.CompanyNumber == id);

        var nameCompany = new DataSignInCompanyViewModel();
        if (checkingCompany != null)
        {
            nameCompany.Name = checkingCompany.Name;
        }
        
        return View(nameCompany);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateViewModel companyCM)
    {
        if (!ModelState.IsValid) return RedirectToAction("CreateCompany", "Company", companyCM);
        
        var userCurrent = await _userManager.GetUserAsync(HttpContext.User);
        if (userCurrent == null) return RedirectToAction("Login", "Account");

        string cNumber = _companyNumber.Main();
        var protocol = Environment.GetEnvironmentVariable("PROTOCOL");
        var domain = Environment.GetEnvironmentVariable("DOMAIN");
        var port = Environment.GetEnvironmentVariable("PORT");
        string referralLink = $"{protocol}://{domain}:{port}/Company/SigninCompany/{cNumber}";
        
        var company = new AppCompany()
        {
            Name = companyCM.Name,
            CompanyNumber = cNumber,
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
                var user = await _context.AppUser.FindAsync(userCurrent.Id);
                if (user != null) user.Company = company.CompanyNumber;
                await _context.SaveChangesAsync();
            }
        }
        
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(DataSignInCompanyViewModel signInVM, string id)
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        if (currentUser == null) return RedirectToAction("Login", "Account");
        
        if (!ModelState.IsValid) return RedirectToAction("SigninCompany", "Company", signInVM);

        var dataAppCompany = _context.AppCompany.FirstOrDefault(current => current.CompanyNumber == id);
        if (dataAppCompany == null) return RedirectToAction("SigninCompany", "Company", signInVM);
        
        // security
        if (signInVM.SecretWord != dataAppCompany.SecretWord ||
            signInVM.Password != dataAppCompany.Password)
        {
            signInVM.ErrorMessage = "Incorrect login information";
            return RedirectToAction("SigninCompany", "Company", signInVM);
        }
        else
        {
            await using (_context)
            {
                var user = await _context.AppUser.FindAsync(currentUser.Id);
                if (user != null) user.Company = id;

                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("Index", "Home");
    }
}