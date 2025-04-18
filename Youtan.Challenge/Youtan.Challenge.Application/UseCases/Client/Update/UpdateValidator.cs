using FluentValidation;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Client.Update;

public class UpdateValidator : AbstractValidator<RequestUpdateClient>
{
    public UpdateValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Nome do usuário não pode estar em branco");
        RuleFor(c => c.Email).NotEmpty().WithMessage("E-mail do usuário não pode estar em branco");
        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage("E-mail do usuário é inválido");
        });
    }
}