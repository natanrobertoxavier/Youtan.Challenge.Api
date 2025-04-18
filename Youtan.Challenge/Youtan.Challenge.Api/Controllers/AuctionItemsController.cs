using Microsoft.AspNetCore.Mvc;
using Youtan.Challenge.Api.Filters;
using Youtan.Challenge.Application.UseCases.Auction.Recover.RecoverAll;
using Youtan.Challenge.Application.UseCases.AuctionItems.Recover.RecoverAll;
using Youtan.Challenge.Application.UseCases.AuctionItems.Register;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Api.Controllers;

public class AuctionItemsController : YoutanController
{
    [HttpPost]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAuctionItemsAsync(
        [FromServices] IRegisterAuctionItemsUseCase useCase,
        [FromBody] RequestRegisterAuctionItems request)
    {
        var result = await useCase.RegisterAuctionItemsAsync(request);

        return ResponseCreate(result);
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<IEnumerable<ResponseAuction>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<ResponseAuction>>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<IEnumerable<ResponseAuction>>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RecoverAllAsync(
        [FromServices] IRecoverAllAuctionItemUseCase useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5)
    {
        var result = await useCase.RecoverAllAsync(page, pageSize);

        return Response(result);
    }
}
