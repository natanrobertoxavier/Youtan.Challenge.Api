using FluentValidation;
using Youtan.Challenge.Communication.Request;

namespace Youtan.Challenge.Application.UseCases.Client.Register;

public class RegisterValidator : AbstractValidator<RequestRegisterClient>
{
    public RegisterValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Nome do usuário não pode estar em branco");
        RuleFor(c => c.Email).NotEmpty().WithMessage("E-mail do usuário não pode estar em branco");
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());
        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage("E-mail do usuário é inválido");
        });
    }
}