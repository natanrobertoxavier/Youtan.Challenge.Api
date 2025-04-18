namespace Youtan.Challenge.Domain.Entities;
public class Client : BaseEntity
{
    public Client(
    Guid id,
    DateTime registrationDate,
    string name,
    string email,
    string password) : base(id, registrationDate)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public Client(
    string name,
    string email,
    string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public Client()
    {
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}