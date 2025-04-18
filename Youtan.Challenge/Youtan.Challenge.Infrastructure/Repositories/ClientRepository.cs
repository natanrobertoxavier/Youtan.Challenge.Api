using Microsoft.EntityFrameworkCore;
using Youtan.Challenge.Domain.Entities;
using Youtan.Challenge.Domain.Repositories.Contracts.Client;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class ClientRepository(YoutanContext context) : IClientWriteOnly, IClientReadOnly
{
    private readonly YoutanContext _context = context;

    public async Task AddAsync(Client client) =>
        await _context.AddAsync(client);

    public void Update(Client client) =>
        _context.Clients.Update(client);

    public bool Remove(Guid clientId)
    {
        var @return = false;
        var clientToRemove = _context.Clients
            .Where(x => x.Id == clientId)
            .FirstOrDefault();

        if (clientToRemove is not null) 
        {
            _context.Clients.Remove(clientToRemove);
            @return = true;
        }

        return @return;
    }

    public async Task<Client?> RecoverByIdAsync(Guid id) =>
        await _context.Clients
        .FirstOrDefaultAsync(d => d.Id.Equals(id));

    public async Task<Client?> RecoverByEmailAsync(string email) =>
        await _context.Clients
        .AsNoTracking()
        .FirstOrDefaultAsync(d => d.Email.Equals(email));

    public async Task<Client?> RecoverByEmailPasswordAsync(string email, string password) =>
        await _context.Clients
        .AsNoTracking()
        .FirstOrDefaultAsync(
            d => d.Email.Equals(email) &&
            d.Password.Equals(password));

    public async Task<IEnumerable<Client>> RecoverAllAsync(int skip, int pageSize) =>
        await _context.Clients
        .AsNoTracking()
        .Skip(skip)
        .Take(pageSize)
        .ToListAsync();
}