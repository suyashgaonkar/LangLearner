using LangLearner.Exceptions;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;

namespace LangLearner.Database.Repositories
{
    public interface IUserRepository
    {
        User? GetUserById(int id);
        User? GetUserByEmail(string email);
        User AddUser(User user);
    }
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;


        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            if (_context.Users.FirstOrDefault(u => user.Email == u.Email) != null) throw new GeneralAPIException("User with provided data already exists") { StatusCode = 409};
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
