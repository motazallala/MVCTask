using System.ComponentModel.DataAnnotations;

namespace MVCTask.MVC.Models;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}