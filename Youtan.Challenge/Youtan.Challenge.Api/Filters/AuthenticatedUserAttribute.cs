using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using TokenService.Manager.Controller;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Domain.Repositories.Contracts.User;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Api.Filters;

public class AuthenticatedUserAttribute(
    TokenController tokenController,
    IUserReadOnly userReadOnlyrepository) : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController = tokenController;
    private readonly IUserReadOnly _userReadOnlyrepository = userReadOnlyrepository;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenInRequest(context);
            var userEmail = _tokenController.RecoverEmail(token);

            var user = await _userReadOnlyrepository.RecoverByEmailAsync(userEmail);

            if (user is null)
            {
                throw new ValidationException("Usuário não localizado para o token informado");
            }
        }
        catch (SecurityTokenExpiredException)
        {
            ExpiredToken(context);
        }
        catch
        {
            UserWithoutPermission(context);
        }
    }

    private static string TokenInRequest(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new YoutanException(string.Empty);
        }

        return authorization["Bearer".Length..].Trim();
    }

    private static void ExpiredToken(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseError("Token expirado"));
    }

    private static void UserWithoutPermission(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseError("Usuário sem permissão"));
    }
}
