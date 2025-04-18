using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.AuctionItems.Register;

public class RegisterValidator : AbstractValidator<RequestRegisterAuctionItems>
{
    public RegisterValidator()
    {
        RuleFor(c => c.ItemType)
            .IsInEnum().WithMessage("O tipo do item deve ser válido");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("A descrição do item não pode estar em branco")
            .MaximumLength(500).WithMessage("A descrição do item não pode exceder 500 caracteres");

        RuleFor(c => c.AuctionId)
            .NotEmpty().WithMessage("O ID do leilão não pode estar em branco")
            .NotEqual(Guid.Empty).WithMessage("O ID do leilão deve ser um GUID válido");
    }
}
