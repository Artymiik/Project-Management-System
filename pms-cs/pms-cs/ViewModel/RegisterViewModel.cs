using System.ComponentModel.DataAnnotations;

namespace pms_cs.ViewModel;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email address is required!")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required!")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}