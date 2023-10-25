using FluentValidation;

namespace Bookify.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(p=> p.FirstName).NotEmpty();
        RuleFor(p=> p.LastName).NotEmpty();
        RuleFor(p=> p.Email).EmailAddress();
        RuleFor(p=> p.Password).NotEmpty().MinimumLength(5);
    }
}
