namespace pms_cs.Interfaces.Details;

public interface IDetailsSetModel
{
    int Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    string Beginning { get; set; }
    string Ending { get; set; }
    string UserNameSending { get; set; }
    string Company { get; set; }
    string Status { get; set; }
}