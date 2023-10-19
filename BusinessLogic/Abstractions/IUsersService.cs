using Domain.Models;
using Services.ResponseDtos;

namespace Services.Abstractions;

public interface IUsersService
{
    Task<ListResponse<GetUsersDto>> GetQueriedAsync(Query query);
    Task<User> GetByIdAsync(int userId);
    Task<GetUsersDto> GetUserDtoByIdAsync(int userId);
    Task<User> GetByNameAsync(string username);
    Task RemoveAsync(User user);
    Task AddUserToRoleAsync(User user, string roleName);
}