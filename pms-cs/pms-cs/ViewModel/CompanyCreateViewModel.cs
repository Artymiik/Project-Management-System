using System.ComponentModel.DataAnnotations;
using pms_cs.Interfaces.Company;

namespace pms_cs.ViewModel;

public class CompanyCreateViewModel : ICompanyCreateViewModel
{
    private string? _name;
    private string? _secretWord;
    private string? _password;

    [Required(ErrorMessage = "Name is required")]
    public string Name
    {
        get => _name ?? string.Empty;
        set => _name = value;
    }
    [Required(ErrorMessage = "Secret word is required")]
    public string SecretWord
    {
        get => _secretWord ?? string.Empty;
        set => _secretWord = value;
    }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password
    {
        get => _password ?? string.Empty;
        set => _password = value;
    }
}