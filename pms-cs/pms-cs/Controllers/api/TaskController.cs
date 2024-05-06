using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pms_cs.Data;
using pms_cs.Interfaces;
using pms_cs.Models;
using pms_cs.ViewModel;

namespace pms_cs.Controllers.api;

public class TaskController : Controller
{
    private readonly AbsApplicationRepository _absApplicationRepository;
    private UserManager<AppUser> _userManager;

    public TaskController(AbsApplicationRepository absApplicationRepository, UserManager<AppUser> userManager)
    {
        _absApplicationRepository = absApplicationRepository;
        _userManager = userManager;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(TaskCreateViewModel taskViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home", taskViewModel);
        var userCurrent = await _userManager.GetUserAsync(HttpContext.User);
        
        var add = _absApplicationRepository.CreateTask(taskViewModel, userCurrent ?? new AppUser());
    }
}