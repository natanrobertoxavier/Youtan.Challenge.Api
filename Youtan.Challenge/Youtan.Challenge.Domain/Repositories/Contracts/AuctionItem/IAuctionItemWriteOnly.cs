namespace Youtan.Challenge.Domain.Repositories.Contracts.AuctionItem;

public interface IAuctionItemWriteOnly
{
    Task AddAsync(Entities.AuctionItem acutionItem);
    void Update(Entities.AuctionItem acutionItem);
    bool Remove(Guid acutionItemId);
}
