using Serilog;
using Youtan.Challenge.Application.Services;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts.Auction;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Exceptions.ExceptionBase;
using Youtan.Challenge.Domain.Repositories.Contracts.AuctionItem;
using Youtan.Challenge.Application.Mapping;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Register;

public class RegisterAuctionItemsUseCase(
    IAuctionReadOnly auctionReadOnlyRepository,
    IAuctionItemWriteOnly auctionItemWriteOnlyRepository,
    ILoggedUser loggedUser,
    IWorkUnit workUnit,
    ILogger logger) : IRegisterAuctionItemsUseCase
{
    private readonly IAuctionReadOnly _auctionReadOnlyRepository = auctionReadOnlyRepository;
    private readonly IAuctionItemWriteOnly _auctionItemWriteOnlyRepository = auctionItemWriteOnlyRepository;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;

    public async Task<Result<MessageResult>> RegisterAuctionItemsAsync(RequestRegisterAuctionItem request)
    {
        var output = new Result<MessageResult>();

        try
        {
            _logger.Information($"Início {nameof(RegisterAuctionItemsAsync)}.");

            await Validate(request);

            var loggedUser = await _loggedUser.GetLoggedUserAsync();

            var auctionItem = request.ToEntity(loggedUser.Id);

            await _auctionItemWriteOnlyRepository.AddAsync(auctionItem);

            await _workUnit.CommitAsync();

            output.Succeeded(new MessageResult("Cadastro realizado com sucesso"));

            _logger.Information($"Fim {nameof(RegisterAuctionItemsAsync)}.");
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

    private async Task Validate(RequestRegisterAuctionItem request)
    {
        _logger.Information($"Início {nameof(Validate)}.");

        var auctionValidator = new RegisterValidator();
        var validationResult = auctionValidator.Validate(request);

        var auction = await _auctionReadOnlyRepository.RecoverByIdAsync(request.AuctionId);

        if (auction is null)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("auction", "Leilão não localizado com os dados informados"));

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
