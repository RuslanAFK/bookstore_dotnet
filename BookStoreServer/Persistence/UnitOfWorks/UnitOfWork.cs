using BookStoreServer.Core;

namespace BookStoreServer.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}