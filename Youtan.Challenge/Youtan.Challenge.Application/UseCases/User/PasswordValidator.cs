using FluentValidation;

namespace Youtan.Challenge.Application.UseCases.User;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage("Senha do usuário não pode estar em branco");
        When(password => !string.IsNullOrWhiteSpace(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage("Senha do usuário deve conter ao menos seis caracteres");
        });
    }
}