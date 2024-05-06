namespace pms_cs.Interfaces.Company;

public interface ICompanyCreateViewModel
{
    string Name { get; set; }
    string SecretWord { get; set; }
    string Password { get; set; }
}