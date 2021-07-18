using System.ComponentModel.DataAnnotations;

namespace BlazorBattle2.Shared
{
    public class UserLogin
    {  
        [Required(ErrorMessage = "Please enter a UserName")]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}