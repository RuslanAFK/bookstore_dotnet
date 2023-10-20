using Domain.Models;
using Services.Dtos;

namespace Services.Abstractions;

public interface IBooksService
{
    Task<ListResponse<GetBooksDto>> GetQueriedAsync(Query query);
    Task<GetSingleBookDto> GetSingleBookDtoByIdAsync(int bookId);
    Task AddAsync(Book bookToCreate);
    Task UpdateAsync(int bookId, BookDto updateBook);
    Task RemoveAsync(int bookId);
}