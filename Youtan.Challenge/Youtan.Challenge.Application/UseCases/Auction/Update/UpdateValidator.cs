using FluentValidation;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Auction.Update;

public class UpdateValidator : AbstractValidator<RequestUpdateAuction>
{
    public UpdateValidator()
    {
        RuleFor(c => c.AuctionId)
            .NotEmpty().WithMessage("É necessário informar o leilão");

        RuleFor(c => c.AuctionName)
            .NotEmpty().WithMessage("Nome do leilão não pode estar em branco");

        RuleFor(c => c.AuctionDescription)
            .NotEmpty().WithMessage("Descrição do leilão não pode estar em branco");

        RuleFor(c => c.AuctionDate)
            .NotEmpty().WithMessage("Data do leilão não pode estar em branco")
            .Must(BeAValidDate).WithMessage("Data do leilão deve ser uma data válida")
            .GreaterThan(DateTime.Now).WithMessage("Data do leilão deve ser no futuro");

        RuleFor(c => c.AuctionAddress)
            .NotEmpty().WithMessage("Endereço do leilão não pode estar em branco");
    }
    private bool BeAValidDate(DateTime date)
    {
        return date != default;
    }
}