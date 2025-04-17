namespace Youtan.Challenge.Communication.Request;

public class RequestLogin(
    string email,
    string password)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}