using Microsoft.EntityFrameworkCore;
using Youtan.Challenge.Domain.Entities;
using Youtan.Challenge.Domain.Repositories.Contracts;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class ClientRepository(YoutanContext context) : IClientWriteOnly, IClientReadOnly
{
    private readonly YoutanContext _context = context;

    public async Task AddAsync(Client client) =>
        await _context.AddAsync(client);

    public void Update(Client client) =>
        _context.Clients.Update(client);

    public async Task<Client> RecoverByEmailAsync(string email) =>
        await _context.Clients
        .AsNoTracking()
        .FirstOrDefaultAsync(d => d.Email.Equals(email));

    public async Task<Client> RecoverByEmailPasswordAsync(string email, string password) =>
        await _context.Clients
        .AsNoTracking()
        .FirstOrDefaultAsync(
            d => d.Email.Equals(email) &&
            d.Password.Equals(password));
}