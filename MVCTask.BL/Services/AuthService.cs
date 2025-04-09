using MVCTask.BL.Interfaces;
using MVCTask.Core.Model;
using MVCTask.DAL.Interfaces;
using MVCTask.Infrastructure.Helper;
using MVCTask.Infrastructure.ResultPattern;

namespace MVCTask.BL.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> Login(string username, string password)
    {
        var hashedPassword = PasswordHelper.HashPassword(password);
        var user = await _userRepository.AuthenticateUserAsync(username, hashedPassword);
        if (user.IsSuccess)
        {
            return Result<User>.Success(user.Value);
        }
        else
        {
            return ResultHelper.ErrorHandler(user); 
        }
    }

    public async Task<Result<string>> Register(string username, string password, string email)
    {
        var hashedPassword = PasswordHelper.HashPassword(password);
        var user = new User
        {
            Username = username,
            PasswordHash = hashedPassword,
            Email = email,
            IsActive = true
        };
        var result= await _userRepository.RegisterUserAsync(user);
        if (result.IsSuccess)
        {
            return Result<string>.Success(result.Value);
        }
        else
        {
            return ResultHelper.ErrorHandler<string>(result);
        }
    }
}
