using Microsoft.AspNetCore.Mvc;
using System.Net;
using Youtan.Challenge.Application.UseCases.Client.Login;
using Youtan.Challenge.Application.UseCases.Client.Register;
using Youtan.Challenge.Application.UseCases.User.Login;
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

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<ResponseLogin>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ResponseLogin>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<ResponseLogin>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UserLoginAsync(
        [FromServices] IClientLoginUseCase useCase,
        [FromBody] RequestLogin request)
    {
        var result = await useCase.LoginAsync(request);

        return Response(
            result,
            HttpStatusCode.OK,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Unauthorized);
    }
}
