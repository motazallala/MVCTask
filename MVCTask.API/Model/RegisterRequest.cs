using System.ComponentModel.DataAnnotations;

namespace MVCTask.API.Model;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}