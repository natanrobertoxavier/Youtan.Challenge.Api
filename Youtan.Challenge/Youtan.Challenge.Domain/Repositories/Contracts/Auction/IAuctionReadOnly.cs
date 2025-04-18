
namespace Youtan.Challenge.Domain.Repositories.Contracts.Auction;

public interface IAuctionReadOnly
{
    Task<IEnumerable<Entities.Auction>> RecoverAllAsync(int v, int pageSize);
}
