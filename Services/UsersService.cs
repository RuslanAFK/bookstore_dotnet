using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;

namespace Services;

public class UsersService : IUsersService
{
    private readonly IUnitOfWork _unitOfWork;
    public UsersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ListResponse<User>> GetQueriedAsync(Query query)
    {
        return await _unitOfWork.Users.GetQueriedItemsAsync(query);
    }
    
    public async Task<User> GetByIdAsync(int userId)
    {
        return await _unitOfWork.Users.GetByIdIncludingRolesAsync(userId);
    }
    
    public async Task<User> GetByNameAsync(string username)
    {
        return await _unitOfWork.Users.GetByNameIncludingRolesAsync(username);
    }
    
    public async Task RemoveAsync(User user)
    {
        _unitOfWork.Users.Remove(user);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    public async Task AddUserToRoleAsync(User user, string roleName)
    {
        CheckIfRoleIsNotIdentical(user.Role.RoleName, roleName);
        await _unitOfWork.Roles.AssignToRoleAsync(user, roleName);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    private void CheckIfRoleIsNotIdentical(string userRoleName, string roleName)
    {
        if (userRoleName == roleName)
        {
            var propName = nameof(Role.RoleName);
            throw new SameValueAssignException(propName);
        }
    }
}