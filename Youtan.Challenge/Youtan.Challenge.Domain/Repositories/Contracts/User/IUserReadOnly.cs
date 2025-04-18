namespace Youtan.Challenge.Domain.Repositories.Contracts.User;

public interface IUserReadOnly
{
    Task<Entities.User> RecoverByEmailAsync(string email);
    Task<Entities.User> RecoverByEmailPasswordAsync(string email, string password);
    //Task<IEnumerable<Entities.User>> RecoverAllAsync(int page, int pageSize);
    //Task<Entities.User> RecoverByIdAsync(Guid id);
}
