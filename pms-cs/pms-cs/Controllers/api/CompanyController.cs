using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pms_cs.Data;
using pms_cs.Interfaces;
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
    private readonly AbsApplicationRepository _absApplicationRepository;
    private readonly CompanyNumber _companyNumber;

    public CompanyController(ApplicationDbContext context, UserManager<AppUser> userManager, CompanyNumber companyNumber, AbsApplicationRepository absApplicationRepository)
    {
        _context = context;
        _userManager = userManager;
        _companyNumber = companyNumber;
        _absApplicationRepository = absApplicationRepository;
        
        DotNetEnv.Env.Load();
    }

    private static string CreateReferralLink(string cNumber)
    {
        var protocol = Environment.GetEnvironmentVariable("PROTOCOL");
        var domain = Environment.GetEnvironmentVariable("DOMAIN");
        var port = Environment.GetEnvironmentVariable("PORT");
        
        return $"{protocol}://{domain}:{port}/Company/SigninCompany/{cNumber}";
    }
    
    [HttpGet]
    public IActionResult CreateCompany()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> SigninCompany(string id)
    {
        var checkingCompany = _absApplicationRepository.GetFirstByCompanyNumber(id);
        DataSignInCompanyViewModel nameCompany = new DataSignInCompanyViewModel()
        {
            Name = checkingCompany.Name
        };

        TempData["id"] = id;
        return View(nameCompany);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateViewModel companyCM)
    {
        if (!ModelState.IsValid) return View("CreateCompany", companyCM);
        
        var userCurrent = await _userManager.GetUserAsync(HttpContext.User);
        if (userCurrent == null) return RedirectToAction("Login", "Account");

        var cNumber = _companyNumber.Main();
        var referralLink = CreateReferralLink(cNumber);
        
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
            var transaction = await _context.SaveChangesAsync();

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
    public async Task<IActionResult> SignIn(DataSignInCompanyViewModel signInVM)
    {
        var id = TempData["id"]?.ToString();
        Console.WriteLine(id);
        
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        
        if (signInVM.SecretWord == "" || signInVM.Password == "") return View("SigninCompany", signInVM);

        if (id == null)
            return RedirectToAction("SigninCompany", "Company", signInVM);
        
        var dataAppCompany = _absApplicationRepository.GetFirstByCompanyNumber(id);
        
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