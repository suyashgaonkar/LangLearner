using LangLearner.Database;
using LangLearner.Database.Repositories;
using LangLearner.Models.Entities;

namespace LangLearner.Services
{
    public interface ILanguagesService
    {
        IEnumerable<Language> getAvailableLanguages();

        Language? GetLanguage(string value);
    }

    public class LanguagesService : ILanguagesService
    {
        public readonly ILanguageRepository _languageRepository;
        public LanguagesService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public IEnumerable<Language> getAvailableLanguages()
        {
            return _languageRepository.GetAll();
        }

        public Language? GetLanguage(string value)
        {
            return _languageRepository.GetLanguageByAny(value);
        }
    }
}
