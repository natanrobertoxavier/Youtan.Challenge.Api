namespace Youtan.Challenge.Application.Services.User;

public interface ILoggedUser
{
    Task<Domain.Entities.User?> GetLoggedUserAsync();
}
