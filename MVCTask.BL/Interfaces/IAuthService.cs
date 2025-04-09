using MVCTask.Core.Model;
using MVCTask.Infrastructure.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.BL.Interfaces;
public interface IAuthService
{
    Task<Result<User>> Login(string username, string password);
    Task<Result<string>> Register(string username, string password, string email);
}
