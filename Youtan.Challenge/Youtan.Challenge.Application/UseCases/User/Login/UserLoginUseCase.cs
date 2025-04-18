﻿using Serilog;
using TokenService.Manager.Controller;
using Youtan.Challenge.Application.Mapping;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts.User;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.User.Login;

public class UserLoginUseCase(
    IUserReadOnly userReadOnlyrepository,
    PasswordEncryptor passwordEncryptor,
    TokenController tokenController,
    ILogger logger) : IUserLoginUseCase
{
    private readonly IUserReadOnly _userReadOnlyrepository = userReadOnlyrepository;
    private readonly PasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly TokenController _tokenController = tokenController;
    private readonly ILogger _logger = logger;

    public async Task<Result<ResponseLogin>> LoginAsync(RequestLogin request)
    {
        var output = new Result<ResponseLogin>();

        try
        {
            _logger.Information($"Início {nameof(LoginAsync)}.");

            var encryptedPassword = _passwordEncryptor.Encrypt(request.Password);

            var entity = await _userReadOnlyrepository.RecoverByEmailPasswordAsync(request.Email.ToLower(), encryptedPassword);

            if (entity is null)
            {
                _logger.Information($"Fim {nameof(LoginAsync)}. Não foram encontrados dados.");

                throw new InvalidLoginException();
            }
            else
            {
                var token = _tokenController.GenerateToken(entity.Email);

                output.Succeeded(entity.ToResponseLogin(token));

                _logger.Information($"Fim {nameof(LoginAsync)}.");
            }
        }
        catch (InvalidLoginException ex)
        {
            output.Failure(new List<string>() { ex.Message });
        }
        catch (Exception ex)
        {
            var errorMessage = string.Format("Algo deu errado: {0}", ex.Message);

            output.Failure(new List<string>() { errorMessage });

            _logger.Error(ex, errorMessage);
        }

        return output;
    }
}
