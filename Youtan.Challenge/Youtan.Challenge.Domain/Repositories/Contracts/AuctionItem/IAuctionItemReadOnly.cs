
namespace Youtan.Challenge.Domain.Repositories.Contracts.AuctionItem;

public interface IAuctionItemReadOnly
{
    Task<IEnumerable<Entities.AuctionItem>> RecoverAllAsync(int v, int pageSize);
}
