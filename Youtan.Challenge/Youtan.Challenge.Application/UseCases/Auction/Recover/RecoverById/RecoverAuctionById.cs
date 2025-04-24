using Serilog;
using Youtan.Challenge.Application.Mapping;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Domain.Repositories.Contracts.Auction;

namespace Youtan.Challenge.Application.UseCases.Auction.Recover.RecoverById;

public class RecoverAuctionById(
    IAuctionReadOnly auctionReadOnlyRepository,
    ILogger logger) : IRecoverAuctionById
{
    private readonly IAuctionReadOnly _auctionReadOnlyRepository = auctionReadOnlyRepository;
    private readonly ILogger _logger = logger;

    public async Task<Result<ResponseAuction>> RecoverAuctionByIdAsync(Guid auctionId)
    {
        var output = new Result<ResponseAuction>();

        try
        {
            _logger.Information($"Início {nameof(RecoverAuctionByIdAsync)}.");

            var entity = await _auctionReadOnlyRepository.RecoverByIdAsync(auctionId);

            if (entity is null)
            {
                output.Succeeded(null);
                _logger.Information($"{nameof(RecoverAuctionByIdAsync)} - Não foram encontrados dados.");
            }
            else
            {
                output.Succeeded(entity.ToResponse());
            }

            _logger.Information($"Fim {nameof(RecoverAuctionByIdAsync)}.");
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
