namespace pms_cs.Interfaces.Task;

public interface IDataTask
{
    string Title { get; set; }
    string Description { get; set; }
    string UserIdRecipient { get; set; }
    string Ending { get; set; }
}