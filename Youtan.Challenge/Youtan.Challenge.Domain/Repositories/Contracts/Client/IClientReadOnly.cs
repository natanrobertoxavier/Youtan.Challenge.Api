namespace Youtan.Challenge.Domain.Repositories.Contracts.Client;

public interface IClientReadOnly
{
    Task<Entities.Client> RecoverByEmailAsync(string email);
    Task<Entities.Client> RecoverByEmailPasswordAsync(string email, string password);
    //Task<IEnumerable<Entities.User>> RecoverAllAsync(int page, int pageSize);
    //Task<Entities.User> RecoverByIdAsync(Guid id);
}
