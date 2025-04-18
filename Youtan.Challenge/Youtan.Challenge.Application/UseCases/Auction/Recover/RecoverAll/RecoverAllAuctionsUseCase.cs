using Serilog;
using Youtan.Challenge.Application.Mapping;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Domain.Repositories.Contracts.Auction;

namespace Youtan.Challenge.Application.UseCases.Auction.Recover.RecoverAll;

public class RecoverAllAuctionsUseCase(
    IAuctionReadOnly auctionReadOnlyRepository,
    ILogger logger) : IRecoverAllAuctionsUseCase
{
    private readonly IAuctionReadOnly _auctionReadOnlyRepository = auctionReadOnlyRepository;
    private readonly ILogger _logger = logger;

    public async Task<Result<IEnumerable<ResponseAuction>>> RecoverAllAsync(int page, int pageSize)
    {
        var output = new Result<IEnumerable<ResponseAuction>>();

        try
        {
            _logger.Information($"Início {nameof(RecoverAllAsync)}.");

            var entities = await _auctionReadOnlyRepository.RecoverAllAsync(Skip(page, pageSize), pageSize);

            if (!entities.Any())
            {
                output.Succeeded(null);
                _logger.Information($"{nameof(RecoverAllAsync)} - Não foram encontrados dados.");
            }
            else
            {
                output.Succeeded(entities.Select(entity => entity.ToResponse()));
            }

            _logger.Information($"Fim {nameof(RecoverAllAsync)}.");
        }
        catch (Exception ex)
        {
            var errorMessage = string.Format("Algo deu errado: {0}", ex.Message);

            _logger.Error(ex, errorMessage);

            output.Failure(new List<string>() { errorMessage });
        }

        return output;
    }

    private static int Skip(int page, int pageSize) =>
        (page - 1) * pageSize;
}
