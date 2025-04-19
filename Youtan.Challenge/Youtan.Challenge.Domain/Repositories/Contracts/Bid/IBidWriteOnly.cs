namespace Youtan.Challenge.Domain.Repositories.Contracts.Bid;

public interface IBidWriteOnly
{
    Task AddAsync(Entities.Bid bid);
}
