using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Bid.Register;

public interface IRegisterBidUseCase
{
    Task<Result<ResponseBid>> RegisterBidAsync(RequestRegisterBid request);
}
