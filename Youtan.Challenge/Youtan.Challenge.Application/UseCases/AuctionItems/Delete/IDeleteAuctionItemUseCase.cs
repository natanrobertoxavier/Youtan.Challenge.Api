using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Delete;

public interface IDeleteAuctionItemUseCase
{
    Task<Result<MessageResult>> DeleteAuctionItemAsync(Guid auctionItemId);
}
