using System.ComponentModel.DataAnnotations;

namespace BlazorBattle2.Shared
{
    public class UserLogin
    {  
        [Required(ErrorMessage = "Please enter a Email Adress")]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}