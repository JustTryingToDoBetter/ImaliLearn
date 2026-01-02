using ImaliLearn.Domain.Entities;

namespace ImaliLearn.Domain.Repositories;

// Repository interface for User entity
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email); // get user by email
    Task AddAsync(User user); // add a new user
}