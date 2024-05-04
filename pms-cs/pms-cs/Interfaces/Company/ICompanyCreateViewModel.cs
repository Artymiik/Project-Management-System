namespace pms_cs.Interfaces;

public interface ICompanyCreateViewModel
{
    string Name { get; set; }
    string SecretWord { get; set; }
    string Password { get; set; }
}