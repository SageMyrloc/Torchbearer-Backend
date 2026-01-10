using Torchbearer.Application.Interfaces;

namespace Torchbearer.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TorchbearerDbContext _context;

    public UnitOfWork(TorchbearerDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
