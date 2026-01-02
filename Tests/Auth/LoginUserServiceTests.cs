using System;
using System.Threading.Tasks;
using ImaliLearn.Application.Auth;
using ImaliLearn.Domain.Configuration;
using ImaliLearn.Domain.Entities;
using ImaliLearn.Infrastructure.Repositories;
using ImaliLearn.Tests.Helpers;
using Xunit;

public class LoginUserServiceTests
{
    [Fact]
    public async Task Login_fails_with_wrong_password()
    {
        var context = TestDbContextFactory.Create();
        var userRepo = new UserRepository(context);
        var hasher = new PasswordHasher();
        var refreshRepo = new RefreshTokenRepository(context);

        var jwt = new JwtTokenService(Microsoft.Extensions.Options.Options.Create(
            new JwtSettings
            {
                Issuer = "test",
                Audience = "test",
                SecretKey = "TEST_SECRET_KEY_123456789",
                ExpiryMinutes = 60
            }));

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@test.com",
            PasswordHash = hasher.Hash("correct-password")
        };

        await userRepo.AddAsync(user);

        var service = new LoginUserService(userRepo, hasher, jwt, refreshRepo);

        var result = await service.HandleAsync("test@test.com", "wrong-password");

        Assert.False(result.IsSuccess);
    }
}