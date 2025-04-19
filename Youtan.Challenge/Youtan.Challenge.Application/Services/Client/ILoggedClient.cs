namespace Youtan.Challenge.Application.Services.Client;

public interface ILoggedClient
{
    Task<Domain.Entities.Client> GetLoggedClientAsync();
}
