using Domain.Abstractions;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task CompleteOrThrowAsync()
    {
        try
        {
            var stateEntries = await _context.SaveChangesAsync();
            if (stateEntries <= 0)
            {
                throw new OperationNotSuccessfulException();
            }
        }
        catch (DbUpdateException ex)
        {
            throw new OperationNotSuccessfulException(ex.Message);
        }
    }
}