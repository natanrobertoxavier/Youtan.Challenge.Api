using Serilog;
using TokenService.Manager.Controller;
using Youtan.Challenge.Application.Mapping;
using Youtan.Challenge.Application.Services.User;
using Youtan.Challenge.Communication.Reponse;
using Youtan.Challenge.Communication.Request;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.Auction;
using Youtan.Challenge.Exceptions.ExceptionBase;

namespace Youtan.Challenge.Application.UseCases.Auction.Register;

public class RegisterAuctionUseCase(
    IAuctionWriteOnly auctionWriteOnlyRepository,
    ILoggedUser loggedUser,
    IWorkUnit workUnit,
    ILogger logger) : IRegisterAuctionUseCase
{
    private readonly IAuctionWriteOnly _auctionWriteOnlyRepository = auctionWriteOnlyRepository;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IWorkUnit _workUnit = workUnit;
    private readonly ILogger _logger = logger;

    public async Task<Result<MessageResult>> RegisterAuctionAsync(RequestRegisterAuction request)
    {
        var output = new Result<MessageResult>();

        try
        {
            _logger.Information($"Início {nameof(RegisterAuctionAsync)}.");

            Validate(request);

            var loggedUser = await _loggedUser.GetLoggedUserAsync();

            var auction = request.ToEntity(loggedUser.Id);

            await _auctionWriteOnlyRepository.AddAsync(auction);

            await _workUnit.CommitAsync();

            output.Succeeded(new MessageResult("Cadastro realizado com sucesso"));

            _logger.Information($"Fim {nameof(RegisterAuctionAsync)}.");
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

    private void Validate(RequestRegisterAuction request)
    {
        _logger.Information($"Início {nameof(Validate)}.");

        var auctionValidator = new RegisterValidator();
        var validationResult = auctionValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
