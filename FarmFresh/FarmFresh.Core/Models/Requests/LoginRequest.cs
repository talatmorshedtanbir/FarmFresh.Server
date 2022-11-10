using System.ComponentModel.DataAnnotations;

namespace FarmFresh.Core.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
