using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Update;

public interface IUpdateAuctionItemUseCase
{
    Task<Result<MessageResult>> UpdateAuctionItemAsync(RequestUpdateAuctionItem request);
}
