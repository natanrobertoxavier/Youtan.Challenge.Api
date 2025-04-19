using Microsoft.AspNetCore.Mvc;
using Youtan.Challenge.Api.Filters;
using Youtan.Challenge.Application.UseCases.Bid.Register;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Api.Controllers;

public class BidAuctionController : YoutanController
{
    [HttpPost]
    [ServiceFilter(typeof(AuthenticatedClientAttribute))]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAuctionItemsAsync(
        [FromServices] IRegisterBidUseCase useCase,
        [FromBody] RequestRegisterBid request)
    {
        var result = await useCase.RegisterBidAsync(request);

        return ResponseCreate(result);
    }
}
