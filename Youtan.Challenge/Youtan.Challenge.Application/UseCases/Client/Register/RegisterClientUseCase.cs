using Serilog;
using TokenService.Manager.Controller;
using Youtan.Challenge.Application.Mapping;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.Client.Register;

public class RegisterClientUseCase(
    IClientReadOnly clientReadOnlyrepository,
    IClientWriteOnly clientWriteOnlyrepository,
    IWorkUnit workUnit,
    PasswordEncryptor passwordEncryptor,
    ILogger logger) : IRegisterClientUseCase
{
    private readonly IClientReadOnly _clientReadOnlyrepository = clientReadOnlyrepository;
    private readonly IClientWriteOnly _clientWriteOnlyrepository = clientWriteOnlyrepository;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly PasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly ILogger _logger = logger;

    public async Task<Result<MessageResult>> RegisterClientAsync(RequestRegisterClient request)
    {
        var output = new Result<MessageResult>();

        try
        {
            _logger.Information($"Início {nameof(RegisterClientAsync)}.");

            await Validate(request);

            var encryptedPassword = _passwordEncryptor.Encrypt(request.Password);

            var client = request.ToEntity(encryptedPassword);

            await _clientWriteOnlyrepository.AddAsync(client);

            await _workUnit.CommitAsync();

            output.Succeeded(new MessageResult("Cadastro realizado com sucesso"));

            _logger.Information($"Fim {nameof(RegisterClientAsync)}.");
        }
        catch (ValidationErrorsException ex)
        {
            var errorMessage = $"Ocorreram erros de validação: {string.Concat(string.Join(", ", ex.ErrorMessages), ".")}";

            _logger.Error(ex, errorMessage);

            output.Failure(ex.ErrorMessages);
        }
        catch (Exception ex)
        {
            var errorMessage = string.Format("Algo deu errado: {0}", ex.Message);

            _logger.Error(ex, errorMessage);

            output.Failure(new List<string>() { errorMessage });
        }

        return output;
    }

    private async Task Validate(RequestRegisterClient request)
    {
        _logger.Information($"Início {nameof(Validate)}.");

        var clientValidator = new RegisterValidator();
        var validationResult = clientValidator.Validate(request);

        var thereIsWithEmail = await _clientReadOnlyrepository.RecoverByEmailAsync(request.Email);

        if (thereIsWithEmail is not null)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("email", "E-mail já cadastrado"));

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
