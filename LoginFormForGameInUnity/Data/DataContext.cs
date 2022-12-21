using LoginFormForGameInUnity.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginFormForGameInUnity.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<UserRegisterRequest> UsersRegister { get; set; }
        public DbSet<UserLoginRequest> UsersLogin { get; set; }
        public DbSet<ResetPasswordRequest> PasswordsReset { get; set; }
        public DbSet<UserScore> UserScores { get; set; }

    }
}
