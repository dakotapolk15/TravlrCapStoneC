
using travlr.Backend.Models;
using travlr.Backend.Data;
using Microsoft.EntityFrameworkCore;
namespace travlr.Backend.Repositories
    
{
    public class UserRepository 
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUserByEmail(string email)
        {
            return await _context.Users.ToListAsync();
        }


        public async Task CreateUser(User user) 
        {
        
        }
    }
}
