using Microsoft.AspNetCore.Mvc;
using System.Net;
using Youtan.Challenge.Api.Filters;
using Youtan.Challenge.Application.UseCases.Client.Delete;
using Youtan.Challenge.Application.UseCases.Client.Login;
using Youtan.Challenge.Application.UseCases.Client.Recover.RecoverAll;
using Youtan.Challenge.Application.UseCases.Client.Register;
using Youtan.Challenge.Application.UseCases.Client.Update;
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

    [HttpGet]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<ResponseClient>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ResponseClient>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<ResponseClient>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RecoverAllAsync(
        [FromServices] IRecoverAllClientUseCase useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5)
    {
        var result = await useCase.RecoverAllAsync(page, pageSize);

        return Response(result);
    }

    [HttpPut]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<ResponseClient>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ResponseClient>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<ResponseClient>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateClientAsync(
        [FromServices] IUpdateClienteUseCase useCase,
        [FromBody] RequestUpdateClient request)
    {
        var result = await useCase.UpdateClientAsync(request);

        return Response(result);
    }

    [HttpDelete("{clientId}")]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteClientAsync(
        [FromServices] IDeleteClientUseCase useCase,
        [FromRoute] Guid clientId)
    {
        var result = await useCase.DeleteClientAsync(clientId);

        return Response(result);
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
