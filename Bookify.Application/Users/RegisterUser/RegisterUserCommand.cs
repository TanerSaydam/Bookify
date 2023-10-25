using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Users;

namespace Bookify.Application.Users.RegisterUser;
public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password): ICommand<UserId>;
