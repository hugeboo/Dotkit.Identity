using Dotkit.Identity.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Persistence.Repository
{
    internal sealed class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityDbContext _ctx;

        public IdentityRepository(IdentityDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUserByName(string username)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User?> GetUserByRefreshToken(string refreshToken)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<List<UserClaim>> GetUserClaims(int userId)
        {
            return await _ctx.UserClaims.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
