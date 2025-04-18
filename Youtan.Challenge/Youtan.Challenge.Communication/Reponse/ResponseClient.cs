namespace Youtan.Challenge.Communication.Reponse;

public class ResponseClient
{
    public ResponseClient(
    Guid doctorId,
    DateTime registrationDate,
    string name,
    string email)
    {
        ClientId = doctorId;
        RegistrationDate = registrationDate;
        Name = name;
        Email = email;
    }

    public ResponseClient()
    {
    }

    public Guid ClientId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}