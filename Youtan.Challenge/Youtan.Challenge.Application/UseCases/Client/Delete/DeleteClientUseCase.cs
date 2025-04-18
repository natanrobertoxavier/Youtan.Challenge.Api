using Serilog;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.Client.Delete;

public class DeleteClientUseCase(
    IClientWriteOnly clientWriteOnlyRepository,
    IWorkUnit workUnit,
    ILogger logger) : IDeleteClientUseCase
{
    private readonly IClientWriteOnly _clientWriteOnlyRepository = clientWriteOnlyRepository;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;
    public async Task<Result<MessageResult>> DeleteClientAsync(Guid clientId)
    {
        var output = new Result<MessageResult>();

        try
        {
            _logger.Information($"Início {nameof(DeleteClientAsync)}."); ;

            var result = _clientWriteOnlyRepository.Remove(clientId);

            if (result)
            {
                await _workUnit.CommitAsync();
                output.Succeeded(new MessageResult("Remoção feita com sucesso."));
            }
            else
                output.Failure(new List<string>() { "Nenhum cliente encontrado com os dados informados." });

            _logger.Information($"Fim {nameof(DeleteClientAsync)}.");
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
}
