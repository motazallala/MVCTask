using MVCTask.Core.Model;
using MVCTask.Infrastructure.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.DAL.Interfaces;
public interface IRoleRepository
{
    Task<Result<List<Role>>> GetAllRolesAsync();
    Task<Result<Role>> GetRoleByIdAsync(int id);
    Task<Result> AddRoleAsync(Role role);
    Task<Result> UpdateRoleAsync(Role role);
    Task<Result> DeleteRoleAsync(int id);
}
