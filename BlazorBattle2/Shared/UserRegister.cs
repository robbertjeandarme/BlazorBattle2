using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorBattle2.Shared
{
    public class UserRegister
    {
        [Required , EmailAddress]
        public string Email { get; set; }
        
        [StringLength(16 , ErrorMessage = "Ur username is too long (16 characters is the max )")]
        public string UserName { get; set; }

        [Required , StringLength(50 , MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password" , ErrorMessage = "Passwords do not match !")]
        public string ConfirmPassword { get; set; }
        
        public string Bio { get; set; }

        public int StartUnitId { get; set; }

        [Range(0 , 1000 , ErrorMessage = "please choose a number between 0 and 1000 !")]
        public int Bananas { get; set; } = 100;

        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Range(typeof(bool) , "true" , "true" , ErrorMessage = "Only confirmed users can play")]
        public bool IsConfirmed { get; set; } = true;
    }
}