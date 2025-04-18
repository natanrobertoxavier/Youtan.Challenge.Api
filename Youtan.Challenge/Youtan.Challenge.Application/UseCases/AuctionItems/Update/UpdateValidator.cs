using FluentValidation;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Update;

public class UpdateValidator : AbstractValidator<RequestUpdateAuctionItem>
{
    public UpdateValidator()
    {
        RuleFor(c => c.ItemType)
            .IsInEnum().WithMessage("O tipo do item deve ser válido");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("A descrição do item não pode estar em branco")
            .MaximumLength(500).WithMessage("A descrição do item não pode exceder 500 caracteres");

        RuleFor(c => c.StartingBid)
            .GreaterThan(0).WithMessage("O valor inicial do item deve ser maior que zero");
    }
}
