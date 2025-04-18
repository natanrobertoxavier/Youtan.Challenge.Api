using Microsoft.EntityFrameworkCore;
using Youtan.Challenge.Domain.Entities;
using Youtan.Challenge.Domain.Repositories.Contracts.AuctionItem;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class AuctionItemRepository(YoutanContext context) : IAuctionItemWriteOnly, IAuctionItemReadOnly
{
    private readonly YoutanContext _context = context;

    public async Task AddAsync(AuctionItem acutionItem) =>
        await _context.AuctionItems.AddAsync(acutionItem);

    public void Update(AuctionItem acutionItem) =>
        _context.AuctionItems.Update(acutionItem);

    public async Task<IEnumerable<AuctionItem>> RecoverAllAsync(int page, int pageSize) =>
        await _context.AuctionItems
        .Skip(page)
        .Take(pageSize)
        .ToListAsync();
}
