using Microsoft.AspNetCore.Mvc;
using Youtan.Challenge.Api.Filters;
using Youtan.Challenge.Application.UseCases.Auction.Recover.RecoverAll;
using Youtan.Challenge.Application.UseCases.Auction.Register;
using Youtan.Challenge.Application.UseCases.Client.Recover.RecoverAll;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Api.Controllers;

public class AuctionController : YoutanController
{
    [HttpPost]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAuctionAsync(
        [FromServices] IRegisterAuctionUseCase useCase,
        [FromBody] RequestRegisterAuction request)
    {
        var result = await useCase.RegisterAuctionAsync(request);

        return ResponseCreate(result);
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthenticatedAttribute))]
    [ProducesResponseType(typeof(Result<IEnumerable<ResponseAuction>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<ResponseAuction>>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<IEnumerable<ResponseAuction>>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RecoverAllAsync(
        [FromServices] IRecoverAllAuctionsUseCase useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5)
    {
        var result = await useCase.RecoverAllAsync(page, pageSize);

        return Response(result);
    }
}
