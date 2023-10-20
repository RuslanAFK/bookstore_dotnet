using Domain.Models;
using Services.Dtos;

namespace Services.Abstractions;

public interface IUsersService
{
    Task<ListResponse<GetUsersDto>> GetQueriedAsync(Query query);
    Task<GetUsersDto> GetUserDtoByIdAsync(int userId);
    Task RemoveAsync(int userId);
    Task AddUserToRoleAsync(int userId, string roleName);
}