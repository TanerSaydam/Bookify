using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.Users.LoginUser;
internal sealed class LoginUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenReponse>
{
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenReponse>> Handle(
        LogInUserCommand request, 
        CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFaliure)
        {
            return Result.Failure<AccessTokenReponse>(UserErrors.InvalidCredentials);
        }

        return new AccessTokenReponse(result.Value);
    }
}
