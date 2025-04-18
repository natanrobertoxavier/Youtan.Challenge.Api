namespace Youtan.Challenge.Communication.Request;

public class RequestUpdateClient(
    Guid clientId,
    string name,
    string email)
{
    public Guid ClientId { get; set; } = clientId;
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
}