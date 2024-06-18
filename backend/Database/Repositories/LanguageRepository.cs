using LangLearner.Models.Entities;
using System.Linq;

namespace LangLearner.Database.Repositories
{
    public interface ILanguageRepository
    {
        IEnumerable<Language> GetAll();

        Language? GetLanguageByAny(string value);
    }
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AppDbContext _context;

        public LanguageRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Language> GetAll()
        {
            return _context.Languages.AsEnumerable();
        }

        public Language? GetLanguageByAny(string value)
        {
            return _context.Languages
                .FirstOrDefault(l => l.Code == value || l.Name == value || l.NativeName == value);
        }
    }
}
