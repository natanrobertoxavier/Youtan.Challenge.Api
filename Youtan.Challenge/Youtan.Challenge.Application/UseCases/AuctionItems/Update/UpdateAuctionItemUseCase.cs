using Serilog;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.AuctionItem;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Update;

public class UpdateAuctionItemUseCase(
    IAuctionItemReadOnly auctionItemReadOnlyRepository,
    IAuctionItemWriteOnly auctionItemWriteOnlyRepository,
    IWorkUnit workUnit,
    ILogger logger) : IUpdateAuctionItemUseCase
{
    private readonly IAuctionItemReadOnly _auctionItemReadOnlyRepository = auctionItemReadOnlyRepository;
    private readonly IAuctionItemWriteOnly _auctionItemWriteOnlyRepository = auctionItemWriteOnlyRepository;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;

    public async Task<Result<MessageResult>> UpdateAuctionItemAsync(RequestUpdateAuctionItem request)
    {
        _logger.Information($"Início {nameof(UpdateAuctionItemAsync)}.");
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

        _logger.Information($"Fim {nameof(UpdateAuctionItemAsync)}.");
        return output;
    }

    private async Task<Domain.Entities.AuctionItem> ValidateAndPrepareEntityAsync(RequestUpdateAuctionItem request)
    {
        _logger.Information($"Início {nameof(ValidateAndPrepareEntityAsync)}.");

        var validationResult = ValidateRequest(request);

        var auctionItem = await GetExistingAuctionAsync(request.AuctionItemId);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }

        auctionItem.ItemType = (Domain.Enum.ItemType) request.ItemType;
        auctionItem.Description = request.Description.ToUpper();
        auctionItem.StartingBid = request.StartingBid;

        return auctionItem;
    }

    private static FluentValidation.Results.ValidationResult ValidateRequest(RequestUpdateAuctionItem request)
    {
        var clientValidator = new UpdateValidator();
        return clientValidator.Validate(request);
    }

    private async Task<Domain.Entities.AuctionItem> GetExistingAuctionAsync(Guid auctionId)
    {
        var auctionItem = await _auctionItemReadOnlyRepository.RecoverByIdAsync(auctionId) ??
            throw new ValidationErrorsException(new List<string> { "Item do leilão não encontrado" });

        return auctionItem;
    }

    private async Task UpdateAsync(Domain.Entities.AuctionItem auction)
    {
        _auctionItemWriteOnlyRepository.Update(auction);
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
