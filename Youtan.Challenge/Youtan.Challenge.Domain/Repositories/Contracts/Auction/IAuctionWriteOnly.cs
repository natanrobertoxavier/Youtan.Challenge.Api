namespace Youtan.Challenge.Domain.Repositories.Contracts.Auction;

public interface IAuctionWriteOnly
{
    Task AddAsync(Entities.Auction auction);
    void Update(Entities.Auction auction);
    bool Remove(Guid auction);
}
