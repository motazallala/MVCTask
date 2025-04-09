using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.DAL.Const;
public static class SqlProcedureNames
{
    public const string GetUserByUserName = "sp_GetUserByUserName";
    public const string GetUserById = "sp_GetUserById";
    public const string GetUserByEmail = "sp_GetUserByEmail";
    public const string RegisterUser = "sp_RegisterUser";
    public const string AssignRoleToUser = "sp_AssignRoleToUser";
    public const string GetUserRoles = "sp_GetUserRoles";
    public const string AuthenticateUser = "sp_AuthenticateUser";
    public const string GetRoleById = "sp_GetRoleById";

}