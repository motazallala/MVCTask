using MVCTask.BL.Interfaces;
using MVCTask.Core.Model;
using MVCTask.DAL.Interfaces;
using MVCTask.Infrastructure.Helper;
using MVCTask.Infrastructure.ResultPattern;

namespace MVCTask.BL.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result> AssignRoleToUserAsync(int userId, int roleId)
    {
        var userResult = await _userRepository.GetUserByIdAsync(userId);
        if (!userResult.IsSuccess)
        {
            return Result.Failure("User not found.");
        }
        var roleResult = await _roleRepository.GetRoleByIdAsync(roleId);
        if (!roleResult.IsSuccess)
        {
            return Result.Failure("Role not found.");
        }
        if (!roleResult.IsSuccess)
        {
            return Result.Failure("Role not found.");
        }
        var result = await _userRepository.AssignRoleToUserAsync(userId, roleId);
        if (result.IsSuccess)
        {
            return Result.Success();
        }
        else
        {
            return ResultHelper.ErrorHandler(result);
        }
    }

    public Task<Result<User>> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Result<User>> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<User>> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Role>>> GetUserRolesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RegisterUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
