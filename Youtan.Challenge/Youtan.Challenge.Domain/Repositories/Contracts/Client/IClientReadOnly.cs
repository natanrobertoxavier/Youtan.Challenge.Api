namespace Youtan.Challenge.Domain.Repositories.Contracts.Client;

public interface IClientReadOnly
{
    Task<Entities.Client> RecoverByEmailAsync(string email);
    Task<Entities.Client> RecoverByEmailPasswordAsync(string email, string password);
    Task<IEnumerable<Entities.Client>> RecoverAllAsync(int page, int pageSize);
    Task<Entities.Client> RecoverByIdAsync(Guid id);
}
