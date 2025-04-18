using Serilog;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.AuctionItem;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Delete;

public class DeleteAuctionItemUseCase(
    IAuctionItemWriteOnly auctionItemWriteOnlyRepository,
    IWorkUnit workUnit,
    ILogger logger) : IDeleteAuctionItemUseCase
{
    private readonly IAuctionItemWriteOnly _auctionItemWriteOnlyRepository = auctionItemWriteOnlyRepository;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;
    public async Task<Result<MessageResult>> DeleteAuctionItemAsync(Guid auctionItemId)
    {
        var output = new Result<MessageResult>();

        try
        {
            _logger.Information($"Início {nameof(DeleteAuctionItemAsync)}."); ;

            var result = _auctionItemWriteOnlyRepository.Remove(auctionItemId);

            if (result)
            {
                await _workUnit.CommitAsync();
                output.Succeeded(new MessageResult("Remoção feita com sucesso."));
            }
            else
                output.Failure(new List<string>() { "Nenhum item encontrado com os dados informados." });

            _logger.Information($"Fim {nameof(DeleteAuctionItemAsync)}.");
        }
        catch (Exception ex)
        {
            var errorMessage = string.Format("Algo deu errado: {0}", ex.Message);

            _logger.Error(ex, errorMessage);

            output.Failure(new List<string>() { errorMessage });
        }

        return output;
    }
}
