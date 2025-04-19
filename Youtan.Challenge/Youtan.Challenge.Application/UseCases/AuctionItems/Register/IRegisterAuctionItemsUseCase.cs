using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Register;

public interface IRegisterAuctionItemsUseCase
{
    Task<Result<MessageResult>> RegisterBidAsync(RequestRegisterAuctionItem request);
}
