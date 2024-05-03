using System.ComponentModel.DataAnnotations;

namespace pms_cs.ViewModel;

public class CompanyCreateViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Secret word is required")]
    public string SecretWord { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}