using Microsoft.Data.SqlClient;
using MVCTask.Core.Model;
using MVCTask.DAL.Const;
using MVCTask.DAL.Interfaces;
using System.Data;
using MVCTask.DAL.Helper;
using MVCTask.Infrastructure.ResultPattern;

namespace MVCTask.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDatabaseService _databaseService;

    public UserRepository(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    public async Task<Result<User>> AuthenticateUserAsync(string email, string hashedPassword)
    {
        var parameters = new[]
        {
            SqlParameterHelper.CreateParameter(SqlParameterNames.Email, SqlDbType.NVarChar, email),
            SqlParameterHelper.CreateParameter(SqlParameterNames.PasswordHash, SqlDbType.NVarChar, hashedPassword),
        };

        try
        {
            var user = await _databaseService.ExecuteReaderAsync(SqlProcedureNames.AuthenticateUser, async reader =>
            {
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        IsActive = (bool)reader["IsActive"]
                    };
                }

                return null;
            }, parameters);

            if (user == null)
            {
                return Result<User>.Failure("Invalid email or password.");
            }

            return Result<User>.Success(user);
        }
        catch (Exception ex)
        {
            return Result<User>.Failure($"Error during authentication: {ex.Message}");
        }
    }
    public async Task<Result<User>> GetUserByUsernameAsync(string username)
    {
        try
        {
            var parameters = new[]
            {
                new SqlParameter(SqlParameterNames.Username, SqlDbType.NVarChar) { Value = username }
            };

            var user = await _databaseService.ExecuteReaderAsync(SqlProcedureNames.GetUserByUserName, async reader =>
            {
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString()!,
                        Email = reader["Email"].ToString()!,
                        PasswordHash = reader["PasswordHash"].ToString()!,
                        IsActive = (bool)reader["IsActive"]
                    };
                }

                return null;
            }, parameters);

            return user != null
                ? Result<User>.Success(user)
                : Result<User>.Failure("User not found.");
        }
        catch (Exception ex)
        {
            return Result<User>.InternalFailure($"An error occurred: {ex.Message}");
        }
    }

    public async Task<Result<User>> GetUserByIdAsync(int id)
    {
        try
        {
            var parameters = new[]
            {
                new SqlParameter(SqlParameterNames.Id, SqlDbType.Int) { Value = id }
            };

            var user = await _databaseService.ExecuteReaderAsync(SqlProcedureNames.GetUserById, async reader =>
            {
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString()!,
                        Email = reader["Email"].ToString()!,
                        PasswordHash = reader["PasswordHash"].ToString()!,
                        IsActive = (bool)reader["IsActive"]
                    };
                }

                return null;
            }, parameters);

            return user != null
                ? Result<User>.Success(user)
                : Result<User>.Failure("User not found.");
        }
        catch (Exception ex)
        {
            return Result<User>.InternalFailure($"An error occurred: {ex.Message}");
        }
    }

    public async Task<Result<User>> GetUserByEmailAsync(string email)
    {
        try
        {
            var parameters = new[]
            {
                new SqlParameter(SqlParameterNames.Email, SqlDbType.NVarChar) { Value = email }
            };

            var user = await _databaseService.ExecuteReaderAsync(SqlProcedureNames.GetUserByEmail, async reader =>
            {
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString()!,
                        Email = reader["Email"].ToString()!,
                        PasswordHash = reader["PasswordHash"].ToString()!,
                        IsActive = (bool)reader["IsActive"]
                    };
                }

                return null;
            }, parameters);

            return user != null
                ? Result<User>.Success(user)
                : Result<User>.Failure("User not found.");
        }
        catch (Exception ex)
        {
            return Result<User>.InternalFailure($"An error occurred: {ex.Message}");
        }
    }

    public async Task<Result<string>> RegisterUserAsync(User user)
    {
        try
        {
            var parameters = new[]
            {

                SqlParameterHelper.CreateParameter(SqlParameterNames.Username, SqlDbType.NVarChar, user.Username),
                SqlParameterHelper.CreateParameter(SqlParameterNames.Email, SqlDbType.NVarChar, user.Email),
                SqlParameterHelper.CreateParameter(SqlParameterNames.PasswordHash, SqlDbType.NVarChar, user.PasswordHash),
                new SqlParameter("@Message", SqlDbType.NVarChar, 256) { Direction = ParameterDirection.Output }
            };

            await _databaseService.ExecuteNonQueryAsync(SqlProcedureNames.RegisterUser, parameters);
            // Capture the message from the output parameter
            var message = (string)parameters.First(p => p.ParameterName == "@Message").Value;

            // If the message indicates success, return the success result
            if (message == "User registered successfully.")
            {
                return Result<string>.Success(message);
            }
            else
            {
                return Result<string>.Failure(message); // Return the failure message if username/email already exists
            }
        }
        catch (Exception ex)
        {
            return Result<string>.InternalFailure($"Failed to register user: {ex.Message}");
        }
    }

    public async Task<Result> AssignRoleToUserAsync(int userId, int roleId)
    {
        try
        {
            var parameters = new[]
            {
                SqlParameterHelper.CreateParameter(SqlParameterNames.UserId, SqlDbType.NVarChar, userId),
                SqlParameterHelper.CreateParameter(SqlParameterNames.RoleId, SqlDbType.NVarChar, roleId),
            };

            await _databaseService.ExecuteNonQueryAsync(SqlProcedureNames.AssignRoleToUser, parameters);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.InternalFailure($"Failed to assign role: {ex.Message}");
        }
    }

    public async Task<Result<List<Role>>> GetUserRolesAsync(int userId)
    {
        try
        {
            var parameters = new[]
            {
                SqlParameterHelper.CreateParameter(SqlParameterNames.UserId, SqlDbType.NVarChar, userId)
            };

            var roles = await _databaseService.ExecuteReaderAsync(SqlProcedureNames.GetUserRoles, async reader =>
            {
                var result = new List<Role>();

                while (await reader.ReadAsync())
                {
                    result.Add(new Role
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()!
                    });
                }

                return result;
            }, parameters);

            return Result<List<Role>>.Success(roles);
        }
        catch (Exception ex)
        {
            return Result<List<Role>>.InternalFailure($"Failed to fetch roles: {ex.Message}");
        }
    }
}
