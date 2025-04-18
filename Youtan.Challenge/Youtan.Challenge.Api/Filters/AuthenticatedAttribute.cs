using FluentMigrator.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TokenService.Manager.Controller;
using Youtan.Challenge.Domain.Repositories.Contracts.User;
using System.ComponentModel.DataAnnotations;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;
using Youtan.Challenge.Exceptions.ExceptionBase;
using Youtan.Challenge.Communication.Reponse;

namespace Youtan.Challenge.Api.Filters;

public class AuthenticatedAttribute(
    TokenController tokenController,
    IUserReadOnly userReadOnlyRepository,
    IClientReadOnly clientReadOnlyRepository) : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController = tokenController;
    private readonly IUserReadOnly _userReadOnlyrepository = userReadOnlyRepository;
    private readonly IClientReadOnly _clientReadOnlyRepository = clientReadOnlyRepository;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenInRequest(context);
            var email = _tokenController.RecoverEmail(token);

            var user = await _userReadOnlyrepository.RecoverByEmailAsync(email);

            if (user is not null)
                return;

            var client = await _clientReadOnlyRepository.RecoverByEmailAsync(email);

            if (client is not null)
                return;

            throw new ValidationException("Usuário não localizado para o token informado");
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
