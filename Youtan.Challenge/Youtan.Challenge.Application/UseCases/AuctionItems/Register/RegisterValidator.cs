using FluentValidation;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Register;

public class RegisterValidator : AbstractValidator<RequestRegisterAuctionItem>
{
    public RegisterValidator()
    {
        RuleFor(c => c.ItemType)
            .IsInEnum().WithMessage("O tipo do item deve ser válido");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("A descrição do item não pode estar em branco")
            .MaximumLength(500).WithMessage("A descrição do item não pode exceder 500 caracteres");

        RuleFor(c => c.StartingBid)
            .GreaterThan(0).WithMessage("O valor inicial do item deve ser maior que zero");

        RuleFor(c => c.AuctionId)
            .NotEmpty().WithMessage("O ID do leilão não pode estar em branco")
            .NotEqual(Guid.Empty).WithMessage("O ID do leilão deve ser um GUID válido");
    }
}
