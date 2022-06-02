namespace WebApi.Models.Users;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "{0} length must be between {2} and {1} characters.", MinimumLength = 6)]
    public string Password { get; set; }
}