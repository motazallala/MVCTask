using System.ComponentModel.DataAnnotations;

namespace MVCTask.MVC.Models;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}
