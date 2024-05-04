using System.ComponentModel.DataAnnotations;
using pms_cs.Interfaces.Account;

namespace pms_cs.ViewModel;

public class RegisterViewModel : IRegisterViewModel
{
    private string? _email;
    private string? _username;
    private string? _password;

    [Required(ErrorMessage = "Email address is required!")]
    [DataType(DataType.EmailAddress)]
    public string Email
    {
        get => _email ?? string.Empty;
        set => _email = value;
    }
    [Required(ErrorMessage = "Username is required!")]
    public string Username
    {
        get => _username ?? string.Empty;
        set => _username = value;
    }
    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    public string Password
    {
        get => _password ?? string.Empty;
        set => _password = value;
    }
}