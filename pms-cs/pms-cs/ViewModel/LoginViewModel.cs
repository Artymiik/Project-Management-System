using System.ComponentModel.DataAnnotations;

namespace pms_cs.ViewModel;

public class LoginViewModel
{
    [Required(ErrorMessage = "Is required")]
    public string Username_Email { get; set; }
    
    [Required(ErrorMessage = "password is required")]
    public string Password { get; set; }
}