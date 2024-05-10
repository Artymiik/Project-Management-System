using pms_cs.Interfaces.Details;

namespace pms_cs.ViewModel.setModel;

public class DetailsSetModel : IDetailsSetModel
{
    private int _id;
    private string? _title;
    private string? _description;
    private string? _beginning;
    private string? _ending;
    private string? _userNameSending;
    private string? _company;
    private string? _status;

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    public string Title
    {
        get => _title ?? string.Empty;
        set => _title = value;
    }
    public string Description
    {
        get => _description ?? string.Empty;
        set => _description = value;
    }
    public string Beginning
    {
        get => _beginning ?? string.Empty;
        set => _beginning = value;
    }
    public string Ending
    {
        get => _ending ?? string.Empty;
        set => _ending = value;
    }
    public string UserNameSending
    {
        get => _userNameSending ?? string.Empty;
        set => _userNameSending = value;
    }
    public string Company
    {
        get => _company ?? string.Empty;
        set => _company = value;
    }
    public string Status
    {
        get => _status ?? string.Empty;
        set => _status = value;
    }
}