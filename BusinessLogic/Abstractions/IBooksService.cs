using Domain.Models;
using Services.ResponseDtos;

namespace Services.Abstractions;

public interface IBooksService
{
    Task<ListResponse<GetBooksDto>> GetQueriedAsync(Query query);
    Task<Book> GetByIdAsync(int bookId);
    Task<GetSingleBookDto> GetSingleBookDtoByIdAsync(int bookId);
    Task AddAsync(Book bookToCreate);
    Task UpdateAsync(int bookId, Book bookToUpdate);
    Task RemoveAsync(Book book);
}