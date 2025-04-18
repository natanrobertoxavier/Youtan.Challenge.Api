using Serilog;
using TokenService.Manager.Controller;
using Youtan.Challenge.Application.UseCases.Client.Register;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.Client.Update;

public class UpdateClienteUseCase(
    IClientReadOnly clientReadOnlyRepository,
    IClientWriteOnly clientWriteOnlyRepository,
    IWorkUnit workUnit,
    PasswordEncryptor passwordEncryptor,
    ILogger logger) : IUpdateClienteUseCase
{
    private readonly IClientReadOnly _clientReadOnlyRepository = clientReadOnlyRepository;
    private readonly IClientWriteOnly _clientWriteOnlyRepository = clientWriteOnlyRepository;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly PasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly ILogger _logger = logger;

    public async Task<Result<MessageResult>> UpdateClientAsync(RequestUpdateClient request)
    {
        _logger.Information($"Início {nameof(UpdateClientAsync)}.");
        var output = new Result<MessageResult>();

        try
        {
            var entity = await ValidateAndPrepareEntityAsync(request);

            await UpdateClientAsync(entity);

            output.Succeeded(new MessageResult("Cadastro atualizado com sucesso"));
        }
        catch (ValidationErrorsException ex)
        {
            HandleValidationException(output, ex);
        }
        catch (Exception ex)
        {
            HandleUnexpectedException(output, ex);
        }

        _logger.Information($"Fim {nameof(UpdateClientAsync)}.");
        return output;
    }

    private async Task<Domain.Entities.Client> ValidateAndPrepareEntityAsync(RequestUpdateClient request)
    {
        _logger.Information($"Início {nameof(ValidateAndPrepareEntityAsync)}.");

        var validationResult = ValidateRequest(request);

        var user = await GetExistingClientAsync(request.ClientId);

        await EnsureEmailIsUniqueAsync(request.Email, user);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }

        user.Name = request.Name;
        user.Email = request.Email;

        return user;
    }

    private FluentValidation.Results.ValidationResult ValidateRequest(RequestUpdateClient request)
    {
        var clientValidator = new UpdateValidator();
        return clientValidator.Validate(request);
    }

    private async Task<Domain.Entities.Client> GetExistingClientAsync(Guid clientId)
    {
        var user = await _clientReadOnlyRepository.RecoverByIdAsync(clientId) ??
            throw new ValidationErrorsException(new List<string> { "Cliente não encontrado" });

        return user;
    }

    private async Task EnsureEmailIsUniqueAsync(string email, Domain.Entities.Client existingClient)
    {
        var clientWithEmail = await _clientReadOnlyRepository.RecoverByEmailAsync(email);
        if (clientWithEmail is not null && !clientWithEmail.Email.Equals(existingClient.Email, StringComparison.OrdinalIgnoreCase))
        {
            throw new ValidationErrorsException(new List<string> { "E-mail já cadastrado" });
        }
    }

    private async Task UpdateClientAsync(Domain.Entities.Client client)
    {
        _clientWriteOnlyRepository.Update(client);
        await _workUnit.CommitAsync();
    }

    private void HandleValidationException(Result<MessageResult> output, ValidationErrorsException ex)
    {
        var errorMessage = $"Ocorreram erros de validação: {string.Join(", ", ex.ErrorMessages)}.";
        _logger.Error(ex, errorMessage);
        output.Failure(ex.ErrorMessages);
    }

    private void HandleUnexpectedException(Result<MessageResult> output, Exception ex)
    {
        var errorMessage = $"Algo deu errado: {ex.Message}";
        _logger.Error(ex, errorMessage);
        output.Failure(new List<string> { errorMessage });
    }
}
