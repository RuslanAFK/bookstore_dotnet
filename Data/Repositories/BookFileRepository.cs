using Domain.Models;

namespace Data.Repositories
{
    public class BookFileRepository : BaseRepository<BookFile>
    {
        public BookFileRepository(AppDbContext context) : base(context)
        {
        }
    }
}
