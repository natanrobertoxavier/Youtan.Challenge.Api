using Serilog;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.Auction;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.Auction.Delete;

public class DeleteAuctionUseCase(
    IAuctionWriteOnly auctionWriteOnlyRepository,
    IWorkUnit workUnit,
    ILogger logger) : IDeleteAuctionUseCase
{
    private readonly IAuctionWriteOnly _auctionWriteOnlyRepository = auctionWriteOnlyRepository;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;
    public async Task<Result<MessageResult>> DeleteAuctionAsync(Guid auctionId)
    {
        var output = new Result<MessageResult>();

        try
        {
            _logger.Information($"Início {nameof(DeleteAuctionAsync)}."); ;

            var result = _auctionWriteOnlyRepository.Remove(auctionId);

            if (result)
            {
                await _workUnit.CommitAsync();
                output.Succeeded(new MessageResult("Remoção feita com sucesso."));
            }
            else
                output.Failure(new List<string>() { "Nenhum leilão encontrado com os dados informados." });

            _logger.Information($"Fim {nameof(DeleteAuctionAsync)}.");
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
