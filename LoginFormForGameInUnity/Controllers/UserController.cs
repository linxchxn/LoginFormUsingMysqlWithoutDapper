using LoginFormForGameInUnity.Data;
using LoginFormForGameInUnity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using System.Security.Cryptography;

namespace LoginFormForGameInUnity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllUsers() 
        //{
        //    var users = await _context.Users.ToListAsync();
        //    return Ok(users);
        //}

        //User Register Request
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegister)
        {
            if(_context.Users.Any(u => u.Username == userRegister.Username || u.Email == userRegister.Email))
            {
                if (_context.Users.Any(u => u.Username == userRegister.Username))
                    return BadRequest("User already exists.");
                else if (_context.Users.Any(u => u.Email == userRegister.Email))
                    return BadRequest("Email already exists");
            }

            CreatePasswordHash(userRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = userRegister.Username,
                Email = userRegister.Email,
                PasswordHash = passwordHash,
                PasswodSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };
            _context.Users.Add(user); 
            await _context.SaveChangesAsync();
            return Ok("User successfully created!");
        }

        //User Login Request
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest userLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLogin.Username);
            if (user == null)
                return BadRequest("Username not found.");
            if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswodSalt))
                return BadRequest("Password is incorrect.");

            if (user.VerifiedAt == null)
            {
                return BadRequest("User not verified!");
            }

            return Ok($"Welcome back, {user.Username}!:)");
        }

        //User Verify by Email
        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                return BadRequest("Invalid token.");
            }
            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("User verified! :)");
        }

        //Forgot Password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string username, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Username == username);

            if(user == null)
            {
                return BadRequest("User not found.");
            }


            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddMinutes(60);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your password");
        }


        //Reset password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswodSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfully reset.");
        }

        [HttpPost("record-userscore")]
        public async Task<IActionResult> AddUserScore(UserScore userScore)
        {
            if (userScore == null || userScore.UserID <= 0 || userScore.Score <= 0 || userScore.TotalScore <= 0)
            {
                var message = "Invalid ";
                if (userScore != null)
                {
                    if (userScore.Score <= 0)
                    {
                        message += "Score";
                    }
                }

                return BadRequest("Cannot get user score");
            }

            var score = new UserScore
            {
                UserID = userScore.UserID,
                Score = userScore.Score,
                TotalScore= userScore.TotalScore,
                RecordPlayDate = DateTime.Now
            };
            _context.UserScores.Add(score);
            await _context.SaveChangesAsync();
            return Ok("score has been recorded");
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
