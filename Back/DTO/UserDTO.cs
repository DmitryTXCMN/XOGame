using System.ComponentModel.DataAnnotations;

namespace Back.DTO;

public class UserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
