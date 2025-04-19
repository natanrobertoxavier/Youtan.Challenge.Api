using Microsoft.AspNetCore.Mvc;
using Youtan.Challenge.Api.Filters;
using Youtan.Challenge.Application.UseCases.AuctionItems.Delete;
using Youtan.Challenge.Application.UseCases.AuctionItems.Recover.RecoverAll;
using Youtan.Challenge.Application.UseCases.AuctionItems.Register;
using Youtan.Challenge.Application.UseCases.AuctionItems.Update;
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
        [FromBody] RequestRegisterAuctionItem request)
    {
        var result = await useCase.RegisterBidAsync(request);

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

    [HttpPut]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateAuctionItemsAsync(
        [FromServices] IUpdateAuctionItemUseCase useCase,
        [FromBody] RequestUpdateAuctionItem request)
    {
        var result = await useCase.UpdateAuctionItemAsync(request);

        return Response(result);
    }

    [HttpDelete("{auctionItemId}")]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ResponseClient>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAuctionAsync(
        [FromServices] IDeleteAuctionItemUseCase useCase,
        [FromRoute] Guid auctionItemId)
    {
        var result = await useCase.DeleteAuctionItemAsync(auctionItemId);

        return Response(result, failStatusCode: System.Net.HttpStatusCode.NotFound);
    }
}
