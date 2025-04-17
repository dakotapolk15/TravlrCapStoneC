
using travlr.Backend.Models;
using travlr.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace travlr.Backend.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmail(string email);
       
            Task CreateUser(User user);
        
    }
    
}
