using Microsoft.Data.SqlClient;
using MVCTask.Core.Model;
using MVCTask.DAL.ADO;
using MVCTask.DAL.Const;
using MVCTask.DAL.Interfaces;
using MVCTask.Infrastructure.ResultPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.DAL.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly IDatabaseService _databaseService;

    public RoleRepository(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public Task<Result> AddRoleAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteRoleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Role>>> GetAllRolesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Role>> GetRoleByIdAsync(int roleId)
    {
        try
        {
            var parameters = new[]
            {
                new SqlParameter(SqlParameterNames.RoleId, SqlDbType.Int) { Value = roleId }
        };

            var role = await _databaseService.ExecuteReaderAsync(SqlProcedureNames.GetRoleById, async reader =>
            {
                if (await reader.ReadAsync())
                {
                    return new Role
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString()!
                    };
                }

                return null;
            }, parameters);

            return role != null
                ? Result<Role>.Success(role)
                : Result<Role>.Failure("Role not found.");
        }
        catch (Exception ex)
        {
            return Result<Role>.Failure($"An error occurred: {ex.Message}");
        }
    }


    public Task<Result> UpdateRoleAsync(Role role)
    {
        throw new NotImplementedException();
    }
}
