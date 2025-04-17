using Microsoft.AspNetCore.Mvc;
using Youtan.Challenge.Application.UseCases.Client.Register;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Api.Controllers;

public class ClientController : YoutanController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterClientAsync(
        [FromServices] IRegisterClientUseCase useCase,
        [FromBody] RequestRegisterClient request)
    {
        var result = await useCase.RegisterClientAsync(request);

        return ResponseCreate(result);
    }
}
