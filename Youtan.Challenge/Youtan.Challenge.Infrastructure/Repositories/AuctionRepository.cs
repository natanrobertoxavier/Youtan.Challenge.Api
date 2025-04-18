using Microsoft.EntityFrameworkCore;
using Youtan.Challenge.Domain.Entities;
using Youtan.Challenge.Domain.Repositories.Contracts.Auction;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class AuctionRepository(YoutanContext context) : IAuctionWriteOnly, IAuctionReadOnly
{
    private readonly YoutanContext _context = context;

    public async Task AddAsync(Auction auction) =>
        await _context.AddAsync(auction);

    public void Update(Auction auction) =>
        _context.Auctions.Update(auction);

    public bool Remove(Guid clientId)
    {
        var @return = false;
        var auctionToRemove = _context.Auctions
            .Where(x => x.Id == clientId)
            .FirstOrDefault();

        if (auctionToRemove is not null)
        {
            _context.Auctions.Remove(auctionToRemove);
            @return = true;
        }

        return @return;
    }

    public async Task<IEnumerable<Auction>> RecoverAllAsync(int skip, int pageSize) =>
        await _context.Auctions
        .AsNoTracking()
        .Skip(skip)
        .Take(pageSize)
        .ToListAsync();

    public async Task<Auction?> RecoverByIdAsync(Guid auctionId) =>
        await _context.Auctions
        .FirstOrDefaultAsync(x => x.Id == auctionId);
}
