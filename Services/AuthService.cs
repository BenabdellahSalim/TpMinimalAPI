using Microsoft.EntityFrameworkCore;
using TpMinimalAPI.Data;

namespace TpMinimalAPI.Services
{
    public class AuthService
    {
        private readonly ApiDbContext context;

        public AuthService(
            ApiDbContext context)
        {
            this.context = context;
        }
        public async Task<int?> GetUserIdFromToken(HttpContext httpContext)
        {
            var userToken = httpContext.Request.Headers["UserToken"].ToString();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Token == userToken);
            if (user is null) return null;
            return user.Id;
        }

       
    }
}
