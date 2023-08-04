using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;

namespace Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRolesRepository _rolesRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UsersService(IUsersRepository usersRepository, IUnitOfWork unitOfWork, IRolesRepository rolesRepository)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _rolesRepository = rolesRepository;
    }
    public async Task<ListResponse<User>> GetQueriedAsync(Query query)
    {
        return await _usersRepository.GetQueriedItemsAsync(query);
    }
    
    public async Task<User> GetByIdAsync(int userId)
    {
        return await _usersRepository.GetByIdIncludingRolesAsync(userId);
    }
    
    public async Task<User> GetByNameAsync(string username)
    {
        return await _usersRepository.GetByNameIncludingRolesAsync(username);
    }
    
    public async Task RemoveAsync(User user)
    {
        _usersRepository.Remove(user);
        await _unitOfWork.CompleteOrThrowAsync();
    }
    public async Task AddUserToRoleAsync(User user, string roleName)
    {
        CheckIfRoleIsNotIdentical(user.Role.RoleName, roleName);
        await _rolesRepository.AssignToRoleAsync(user, roleName);
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