using FluentValidation;

namespace Youtan.Challenge.Application.UseCases.Client;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage("Senha do cliente não pode estar em branco");
        When(password => !string.IsNullOrWhiteSpace(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage("Senha do cliente deve conter ao menos seis caracteres");
        });
    }
}