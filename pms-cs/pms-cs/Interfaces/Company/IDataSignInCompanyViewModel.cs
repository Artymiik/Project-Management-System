namespace pms_cs.Interfaces.Company;

public interface IDataSignInCompanyViewModel
{
    string Name { get; set; }
    string SecretWord { get; set; }
    string Password { get; set; }
    string ErrorMessage { get; set; }
}