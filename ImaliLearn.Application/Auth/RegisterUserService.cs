using ImaliLearn.Application.Common.Results;
using ImaliLearn.Domain.Entities;
using ImaliLearn.Domain.Repositories;

namespace ImaliLearn.Application.Auth;

public class RegisterUserService
{
    private readonly IUserRepository _users;
    private readonly PasswordHasher _hasher;

    public RegisterUserService(IUserRepository users, PasswordHasher hasher)
    {
        _users = users;
        _hasher = hasher;
    }

    public async Task<Result<Guid>> HandleAsync(string email, string password)
    {
        if (await _users.GetByEmailAsync(email) != null)
            return Result<Guid>.Failure("Email already registered.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = _hasher.Hash(password)
        };

        await _users.AddAsync(user);
        return Result<Guid>.Success(user.Id);
    }
}
