using Microsoft.AspNetCore.Mvc;
using Youtan.Challenge.Api.Filters;
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

}
