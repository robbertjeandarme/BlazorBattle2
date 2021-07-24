using System;
namespace BlazorBattle2.Shared
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public int Bananas { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool IsConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        
    }
}