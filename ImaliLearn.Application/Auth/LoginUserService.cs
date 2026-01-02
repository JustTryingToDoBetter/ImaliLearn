using ImaliLearn.Application.Common.Results;
using ImaliLearn.Domain.Repositories;

namespace ImaliLearn.Application.Auth;

public class LoginUserService
{
    private readonly IUserRepository _users;
    private readonly PasswordHasher _hasher;
    private readonly JwtTokenService _jwt;

    public LoginUserService(
        IUserRepository users,
        PasswordHasher hasher,
        JwtTokenService jwt)
    {
        _users = users;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<Result<string>> HandleAsync(string email, string password)
    {
        var user = await _users.GetByEmailAsync(email);
        if (user == null)
            return Result<string>.Failure("Invalid credentials.");

        if (!_hasher.Verify(password, user.PasswordHash))
            return Result<string>.Failure("Invalid credentials.");

        var token = _jwt.GenerateToken(user.Id, user.Email);
        return Result<string>.Success(token);
    }
}