using Dotkit.Identity.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Persistence.Repository
{
    public interface IIdentityRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByName(string username);
        Task<User?> GetUserByRefreshToken(string refreshToken);
        Task<List<UserClaim>> GetUserClaims(int userId);
        Task SaveChanges();
    }
}
