using System.ComponentModel.DataAnnotations;
using pms_cs.Interfaces.Task;

namespace pms_cs.ViewModel;

public class TaskCreateViewModel : IDataTask
{
    private string? _title;
    private string? _description;
    private string? _userIdRecipient;
    private string? _ending;

    [Required]
    public string Title
    {
        get => _title ?? string.Empty;
        set => _title = value;
    }
    
    [Required]
    [MaxLength(300)]
    public string Description
    {
        get => _description ?? string.Empty;
        set => _description = value;
    }
    [Required]
    public string UserIdRecipient
    {
        get => _userIdRecipient ?? string.Empty;
        set => _userIdRecipient = value;
    }
    [Required]
    public string Ending
    {
        get => _ending ?? string.Empty;
        set => _ending = value;
    }
}