using Data.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Dtos;
using Services.Extensions;

namespace Services.Services;

public class UsersService : IUsersService
{
    private readonly IUnitOfWork _unitOfWork;
    public UsersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ListResponse<GetUsersDto>> GetQueriedAsync(Query query)
    {
        var users = _unitOfWork.Users.GetAll().AsNoTracking();

        var itemsSearched = users
            .ApplySearching(query);
        var itemsPaginatedList = await itemsSearched
            .ApplyPagination(query)
            .ToGetUsersDto()
            .ToListAsync();
        var itemsCount = itemsSearched.Count();
        
        return new ListResponse<GetUsersDto>
        {
            Items = itemsPaginatedList,
            Count = itemsCount
        };
    }
    
    public async Task<User> GetByIdIncludingRolesAsync(int userId)
    {
        return await _unitOfWork.Users.GetByIdIncludingRolesAsync(userId);
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _unitOfWork.Users.GetByIdAsync(userId);
    }

    public async Task<GetUsersDto> GetUserDtoByIdAsync(int userId)
    {
        return await _unitOfWork.Users.GetAll()
                   .AsNoTracking()
                   .ToGetUsersDto()
                   .FirstOrDefaultAsync(x => x.Id == userId)
               ?? throw new EntityNotFoundException(typeof(User), nameof(User.Id));
    }

    public async Task<User> GetByNameIncludingRolesAsync(string username)
    {
        return await _unitOfWork.Users.GetByNameIncludingRolesAsync(username);
    }

    public async Task<User> GetByNameAsync(string username)
    {
        return await _unitOfWork.Users.GetByNameAsync(username);
    }

    public async Task RemoveAsync(User user)
    {
        _unitOfWork.Users.Remove(user);
        await _unitOfWork.CompleteAsync();
    }
    public async Task AddUserToRoleAsync(User user, string roleName)
    {
        var role = await _unitOfWork.Roles.GetByNameAsync(roleName);
        user.Role = role;
        _unitOfWork.Users.Update(user);
        await _unitOfWork.CompleteAsync();
    }
}