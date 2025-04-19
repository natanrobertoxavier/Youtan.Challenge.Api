using Serilog;
using System.Net.Http.Headers;
using Youtan.Challenge.Application.Mapping;
using Youtan.Challenge.Application.Services.Client;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.AuctionItem;
using Youtan.Challenge.Domain.Repositories.Contracts.Bid;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.Bid.Register;

public class RegisterBidUseCase(
    IAuctionItemReadOnly auctionItemReadOnlyRepository,
    IBidReadOnly bidReadOnlyRepository,
    IBidWriteOnly bidWriteOnlyRepository,
    ILoggedClient loggedClient,
    IWorkUnit workUnit,
    ILogger logger) : IRegisterBidUseCase
{
    private readonly IAuctionItemReadOnly _auctionItemReadOnlyRepository = auctionItemReadOnlyRepository;
    private readonly IBidReadOnly _bidReadOnlyRepository = bidReadOnlyRepository;
    private readonly IBidWriteOnly _bidWriteOnlyRepository = bidWriteOnlyRepository;
    private readonly ILoggedClient _loggedClient = loggedClient;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;

    public async Task<Result<ResponseBid>> RegisterBidAsync(RequestRegisterBid request)
    {
        var output = new Result<ResponseBid>();

        try
        {
            _logger.Information($"Início {nameof(RegisterBidAsync)}.");

            await Validate(request);

            var lastBid = await RecoverLastBidAsync(request);

            var loggedClient = await _loggedClient.GetLoggedClientAsync();

            var entity = request.ToEntity(loggedClient.Id, lastBid);

            await _bidWriteOnlyRepository.AddAsync(entity);

            await _workUnit.CommitAsync();

            output.Succeeded(new ResponseBid(entity.Value));

            _logger.Information($"Fim {nameof(RegisterBidAsync)}.");
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

    private async Task Validate(RequestRegisterBid request)
    {
        var auctionItem = await _auctionItemReadOnlyRepository.RecoverByIdAsync(request.AuctionItemId) ??
            throw new ValidationErrorsException(new List<string>() { "Item não localizado com as informações fornecidas" });

        if (request.Value < auctionItem.Increase)
            throw new ValidationErrorsException(new List<string>() { $"Valor do lance não pode ser menor que {auctionItem.Increase}" });
    }

    private async Task<decimal> RecoverLastBidAsync(RequestRegisterBid request)
    {
        _logger.Information($"Início {nameof(RecoverLastBidAsync)}.");

        var auction = await _bidReadOnlyRepository.RecoverBidByAuctionItemIdAsync(request.AuctionItemId);
        
        return auction?.Value ?? 0;
    }
}