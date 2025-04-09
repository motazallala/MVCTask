using MVCTask.Core.Model;
using MVCTask.Infrastructure.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.DAL.Interfaces;
public interface IUserRepository
{
    Task<Result<User>> GetUserByUsernameAsync(string username);
    Task<Result<User>> GetUserByIdAsync(int id);
    Task<Result<User>> GetUserByEmailAsync(string email);
    Task<Result<string>> RegisterUserAsync(User user);
    Task<Result> AssignRoleToUserAsync(int userId, int roleId);
    Task<Result<List<Role>>> GetUserRolesAsync(int userId);
    Task<Result<User>> AuthenticateUserAsync(string email, string hashedPassword);
}
