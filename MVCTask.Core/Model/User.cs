using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTask.Core.Model;
public class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public List<Role> Roles { get; set; } = new List<Role>();
}

