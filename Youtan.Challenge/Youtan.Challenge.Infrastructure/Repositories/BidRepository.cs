using Microsoft.EntityFrameworkCore;
using Youtan.Challenge.Domain.Entities;
using Youtan.Challenge.Domain.Repositories.Contracts.Bid;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class BidRepository(YoutanContext context) : IBidReadOnly, IBidWriteOnly
{
    private readonly YoutanContext _context = context;

    public async Task<Bid?> RecoverBidByAuctionItemIdAsync(Guid auctionItemId) =>
        await _context.Bids
        .OrderByDescending(b => b.RegistrationDate)
        .FirstOrDefaultAsync(b => b.AuctionItemId == auctionItemId);

    public async Task AddAsync(Bid bid) =>
        await _context.Bids.AddAsync(bid);
}
