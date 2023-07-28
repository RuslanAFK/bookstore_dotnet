using Domain.Abstractions;

namespace Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CompleteAsync()
    {
        var stateEntries = await _context.SaveChangesAsync();
        return stateEntries > 0;
    }
}