using Microsoft.EntityFrameworkCore;
using Youtan.Challenge.Domain.Entities;
using Youtan.Challenge.Domain.Repositories.Contracts.User;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class UserRepository(YoutanContext context) : IUserWriteOnly, IUserReadOnly
{
    private readonly YoutanContext _context = context;

    public async Task AddAsync(User doctor) =>
        await _context.AddAsync(doctor);

    public void Update(User doctor) =>
        _context.Users.Update(doctor);

    public async Task<User> RecoverByEmailAsync(string email) =>
        await _context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(d => d.Email.Equals(email));

    public async Task<IEnumerable<User>> RecoverAllAsync(int skip, int pageSize) =>
        await _context.Users
        .AsNoTracking()
        .Skip(skip)
        .Take(pageSize)
        .ToListAsync();

    public async Task<User> RecoverByEmailPasswordAsync(string email, string password) =>
        await _context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(
            d => d.Email.Equals(email) &&
            d.Password.Equals(password));

    public async Task<User> RecoverByIdAsync(Guid id) =>
        await _context.Users
        .FirstOrDefaultAsync(d => d.Id.Equals(id));
}