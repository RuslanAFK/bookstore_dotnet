using Domain.Models;

namespace Domain.Abstractions;

public interface IUnitOfWork
{
    public IBooksRepository Books { get; set; }
    public IUsersRepository Users { get; set; }
    public IRolesRepository Roles { get; set; }
    public IBaseRepository<BookFile> BookFiles { get; set; }
    Task CompleteOrThrowAsync();
}