namespace Youtan.Challenge.Domain.Repositories.Contracts.Client;

public interface IClientWriteOnly
{
    Task AddAsync(Entities.Client user);
    void Update(Entities.Client user);
    bool Remove(Guid clientId);
}
