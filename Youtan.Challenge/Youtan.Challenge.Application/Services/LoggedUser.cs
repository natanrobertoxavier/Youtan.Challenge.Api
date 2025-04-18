using Microsoft.AspNetCore.Http;
using TokenService.Manager.Controller;
using Youtan.Challenge.Domain.Repositories.Contracts.User;

namespace Youtan.Challenge.Application.Services;

public class LoggedUser(
    IHttpContextAccessor httpContextAccessor,
    TokenController tokenController,
    IUserReadOnly repository) : ILoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly TokenController _tokenController = tokenController;
    private readonly IUserReadOnly _repository = repository;

    public async Task<Domain.Entities.User> GetLoggedUserAsync()
    {
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var email = _tokenController.RecoverEmail(token);

        return await _repository.RecoverByEmailAsync(email);
    }
}
