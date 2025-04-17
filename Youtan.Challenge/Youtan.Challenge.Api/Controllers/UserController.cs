using Microsoft.AspNetCore.Mvc;
using Youtan.Challenge.Application.UseCases.User.Register;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Api.Controllers;

public class UserController : YoutanController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUserAsync(
        [FromServices] IRegisterUseCase useCase,
        [FromBody] RequestRegisterUser request)
    {
        var result = await useCase.RegisterUserAsync(request);

        return ResponseCreate(result);
    }

    //[HttpPut("change-password")]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status422UnprocessableEntity)]
    //public async Task<IActionResult> ChangePasswordAsync(
    //    [FromServices] IChangePasswordUseCase useCase,
    //    [FromBody] RequestChangePassword request)
    //{
    //    var result = await useCase.ChangePasswordAsync(request);

    //    return Response(result);
    //}

    //[HttpGet]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status422UnprocessableEntity)]
    //public async Task<IActionResult> RecoverAllAsync(
    //    [FromServices] IRecoverAllUseCase useCase,
    //    [FromQuery] int page = 1,
    //    [FromQuery] int pageSize = 5)
    //{
    //    var result = await useCase.RecoverAllAsync(page, pageSize);

    //    return Response(result);
    //}

    //[HttpGet("{email}")]
    //[ServiceFilter(typeof(AuthenticatedUserAttribute))]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(Result<MessageResult>), StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> RecoverByEmailAsync(
    //    [FromServices] IRecoverByEmailUseCase useCase,
    //    [FromRoute] string email)
    //{
    //    var result = await useCase.RecoverByEmailAsync(email);

    //    return Response(result);
    //}
}
