using Data.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        BookFiles = new BookFileRepository(context);
        Books = new BooksRepository(context);
        Users = new UsersRepository(context);
        Roles = new RolesRepository(context);
    }

    public IBooksRepository Books { get; set; }
    public IUsersRepository Users { get; set; }
    public IRolesRepository Roles { get; set; }
    public IBaseRepository<BookFile> BookFiles { get; set; }

    public async Task CompleteAsync()
    {
        try
        {
            var stateEntries = await _context.SaveChangesAsync();
            if (stateEntries <= 0)
                throw new OperationNotSuccessfulException("Nothing to change");
        }
        catch (DbUpdateException ex)
        {
            throw new OperationNotSuccessfulException(ex.Message);
        }
    }
}