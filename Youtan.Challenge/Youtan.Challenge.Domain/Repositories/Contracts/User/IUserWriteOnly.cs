namespace Youtan.Challenge.Domain.Repositories.Contracts.User;

public interface IUserWriteOnly
{
    Task AddAsync(Entities.User user);
    void Update(Entities.User user);
}
