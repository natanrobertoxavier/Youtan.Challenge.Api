using FluentMigrator.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TokenService.Manager.Controller;
using Youtan.Challenge.Domain.Repositories.Contracts;
using System.ComponentModel.DataAnnotations;
using Youtan.Challenge.Exceptions.ExceptionBase;
using Youtan.Challenge.Communication.Reponse;

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

            if (user?.Id == Guid.Empty)
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
