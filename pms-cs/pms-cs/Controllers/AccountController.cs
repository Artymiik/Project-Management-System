using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pms_cs.Models;
using pms_cs.ViewModel;

namespace pms_cs.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid) return View(loginVM);

        var userEmail = await _userManager.FindByEmailAsync(loginVM.Username_Email);
        var userName = await _userManager.FindByNameAsync(loginVM.Username_Email);

        if (userEmail != null)
        {
            var currentUser = await _signInManager.PasswordSignInAsync(userEmail, loginVM.Password, true, false);
            if (currentUser.Succeeded) return RedirectToAction("Index", "Home");

            TempData["Error"] = "Incorrect data";
            return View(loginVM);
        } 
        else if (userName != null)
        {
            var currentUser = await _signInManager.PasswordSignInAsync(userName, loginVM.Password, true, false);
            if (currentUser.Succeeded) return RedirectToAction("Index", "Home");

            TempData["Error"] = "Incorrect data";
            return View(loginVM);
        }

        TempData["Error"] = "We have not found a account";
        return View(loginVM);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerVM)
    {
        if (!ModelState.IsValid) return View(registerVM);

        var currentUser = await _userManager.FindByEmailAsync(registerVM.Email);
        if (currentUser != null)
        {
            TempData["Error"] = "An account with this email already exists";
            return View(registerVM);
        }

        var userModel = new AppUser()
        {
            Email = registerVM.Email,
            UserName = registerVM.Username,
        };
        
        var user = await _userManager.CreateAsync(userModel, registerVM.Password);
        if (user.Succeeded)
        {
            await _signInManager.SignInAsync(userModel, true);
            return RedirectToAction("Index", "Home");
        }

        TempData["Error"] = "Wrong credentials. Please, try again";
        return View(registerVM);
    }
}