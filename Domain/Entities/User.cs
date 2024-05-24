using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 100 characters")]
    [Column("username")]
    public string UserName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password is required")]
    [StringLength(200, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 200 characters")]
    [Column("hashed_password")]
    public string HashedPassword { get; set; } = string.Empty;
}