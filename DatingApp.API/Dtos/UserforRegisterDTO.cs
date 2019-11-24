using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
namespace DatingApp.API.Dtos
{
    public class UserforRegisterDTO
    {
        [Required]
        public string username { get; set; }

        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="You must specify a password between 4 to 8 characters")]
        public string password { get; set; }

    }
}