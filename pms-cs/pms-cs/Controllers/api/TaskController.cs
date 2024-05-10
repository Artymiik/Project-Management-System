using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pms_cs.Interfaces;
using pms_cs.Models;
using pms_cs.ViewModel;
using pms_cs.ViewModel.setModel;

namespace pms_cs.Controllers.api;

[Authorize]
public class TaskController : Controller
{
    private readonly AbsApplicationRepository _absApplicationRepository;
    private readonly UserManager<AppUser> _userManager;

    public TaskController(AbsApplicationRepository absApplicationRepository, UserManager<AppUser> userManager)
    {
        _absApplicationRepository = absApplicationRepository;
        _userManager = userManager;
    }

    private string convertToStringDateTime(DateTime data)
    {
        string convertToStringDateTime = data.ToString("yyyy-MM-dd HH:mm:ss");
        var instance = DateTime.Parse(convertToStringDateTime);
        
        return instance.ToString("MMMMM dd, yyyy", new CultureInfo("en-US"));
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        AppTask? data = _absApplicationRepository.GetInfoAppTaskById(id);

        var model = new DetailsSetModel()
        {
            Id = data.Id,
            Title = data.Title,
            Description = data.Description,
            Beginning = convertToStringDateTime(data.Beginning),
            Ending = convertToStringDateTime(data.Ending),
            UserNameSending = _absApplicationRepository.GetUsernameSending(data.AppUserIdSend),
            Company = _absApplicationRepository.GetCompanyName(data.CompanyNumber),
            Status = data.Status,
        };
            
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(TaskCreateViewModel taskViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home", taskViewModel);
        var userCurrent = await _userManager.GetUserAsync(HttpContext.User);
        
        if (userCurrent != null)
            await _absApplicationRepository.CreateTask(taskViewModel, userCurrent);

        /*if (!await add)
            return View("Error");*/
        
        return RedirectToAction("Index", "Home");
    }
}