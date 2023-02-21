using System.ComponentModel.DataAnnotations;

namespace Back.DTO;

public class RegisterUserDto : UserDto
{
    [Required]
    public string RepeatPassword { get; set; }
}
