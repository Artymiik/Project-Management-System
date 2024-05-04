using System.ComponentModel.DataAnnotations;
using pms_cs.Interfaces.Account;

namespace pms_cs.ViewModel;

public class LoginViewModel : ILoginViewModel
{
    private string? _username_email;
    private string? _password;

    [Required(ErrorMessage = "Is required")]
    public string Username_Email
    {
        get => _username_email ?? string.Empty;
        set => _username_email = value;
    }
    [Required(ErrorMessage = "password is required")]
    public string Password
    {
        get => _password ?? string.Empty;
        set => _password = value;
    }
}