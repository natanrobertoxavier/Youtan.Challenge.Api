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
}
