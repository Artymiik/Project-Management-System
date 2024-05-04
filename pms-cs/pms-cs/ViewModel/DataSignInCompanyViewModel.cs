using pms_cs.Interfaces.Company;

namespace pms_cs.ViewModel;

public class DataSignInCompanyViewModel : IDataSignInCompanyViewModel
{
    private string? _name;
    private string? _secretWord;
    private string? _password;
    private string? _errorMessage;

    public string Name
    {
        get => _name ?? string.Empty;
        set => _name = value;
    }
    public string SecretWord
    {
        get => _secretWord ?? string.Empty;
        set => _secretWord = value;
    }
    public string Password
    {
        get => _password ?? string.Empty;
        set => _password = value;
    }
    public string ErrorMessage
    {
        get => _errorMessage ?? string.Empty;
        set => _errorMessage = value;
    }
}