using Serilog;
using TokenService.Manager.Controller;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.Auction;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.Auction.Update;

public class UpdateAuctionUseCase(
    IAuctionReadOnly auctionReadOnlyRepository,
    IAuctionWriteOnly auctionWriteOnlyRepository,
    IWorkUnit workUnit,
    ILogger logger) : IUpdateAuctionUseCase
{
    private readonly IAuctionReadOnly _auctionReadOnlyRepository = auctionReadOnlyRepository;
    private readonly IAuctionWriteOnly _auctionWriteOnlyRepository = auctionWriteOnlyRepository;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;

    public async Task<Result<MessageResult>> UpdateAuctionAsync(RequestUpdateAuction request)
    {
        _logger.Information($"Início {nameof(UpdateAuctionAsync)}.");
        var output = new Result<MessageResult>();

        try
        {
            var entity = await ValidateAndPrepareEntityAsync(request);

            await UpdateAsync(entity);

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

        _logger.Information($"Fim {nameof(UpdateAuctionAsync)}.");
        return output;
    }

    private async Task<Domain.Entities.Auction> ValidateAndPrepareEntityAsync(RequestUpdateAuction request)
    {
        _logger.Information($"Início {nameof(ValidateAndPrepareEntityAsync)}.");

        var validationResult = ValidateRequest(request);

        var auction = await GetExistingAuctionAsync(request.AuctionId);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }

        auction.AuctionDate = request.AuctionDate;
        auction.AuctionName = request.AuctionName;
        auction.AuctionDescription = request.AuctionDescription;
        auction.AuctionAddress = request.AuctionAddress;

        return auction;
    }

    private static FluentValidation.Results.ValidationResult ValidateRequest(RequestUpdateAuction request)
    {
        var clientValidator = new UpdateValidator();
        return clientValidator.Validate(request);
    }

    private async Task<Domain.Entities.Auction> GetExistingAuctionAsync(Guid auctionId)
    {
        var auction = await _auctionReadOnlyRepository.RecoverByIdAsync(auctionId) ??
            throw new ValidationErrorsException(new List<string> { "Leilão não encontrado" });

        return auction;
    }

    private async Task UpdateAsync(Domain.Entities.Auction auction)
    {
        _auctionWriteOnlyRepository.Update(auction);
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
