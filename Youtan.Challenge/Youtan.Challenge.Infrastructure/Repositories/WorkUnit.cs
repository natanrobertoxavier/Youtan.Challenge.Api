using Youtan.Challenge.Domain.Repositories.Contracts;

namespace Youtan.Challenge.Infrastructure.Repositories;

public class WorkUnit(
    YoutanContext context) : IDisposable, IWorkUnit
{
    private readonly YoutanContext _context = context;
    private bool _disposed;

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
}