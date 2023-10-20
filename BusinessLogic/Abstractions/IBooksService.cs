using Domain.Models;
using Services.Dtos;

namespace Services.Abstractions;

public interface IBooksService
{
    Task<ListResponse<GetBooksDto>> GetQueriedAsync(Query query);
    Task<Book> GetByIdIncludingFilesAsync(int bookId);
    Task<Book> GetByIdAsync(int bookId);
    Task<GetSingleBookDto> GetSingleBookDtoByIdAsync(int bookId);
    Task AddAsync(Book bookToCreate);
    Task UpdateAsync(Book foundBook, BookDto updateBook);
    Task RemoveAsync(Book book);
}