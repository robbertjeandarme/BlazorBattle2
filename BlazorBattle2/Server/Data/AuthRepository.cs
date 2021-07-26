using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlazorBattle2.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace BlazorBattle2.Server.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int> { Succes = false, Message = "User already exists." };
            }
            
            CreatePasswordHash(password , out byte[] passwordHash , out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> {Data = user.Id, Message = "Registration successful"};
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var respone = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower().Equals(email.ToLower()));

            if (user == null)
            {
                respone.Succes = false;
                respone.Message = "User not found.";
            }
            else if (!VerifyPasswardHash(password , user.PasswordHash , user.PasswordSalt))
            {
                respone.Succes = false;
                respone.Message = "Password was wrong.";
            }
            else
            {
                respone.Succes = true;
                respone.Message = "Login was successfully";
                respone.Data = user.Id.ToString();
            }

            return respone;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswardHash(string password , byte[] passwordHash , byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}