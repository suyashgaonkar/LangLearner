using LangLearner.Database;
using LangLearner.Database.Repositories;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;
using LangLearner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LangLearner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        public readonly ILanguagesService _languagesService;
        public readonly ILanguageRepository _languageRepository;
        public readonly ILogger<LanguagesController> _logger;

        public LanguagesController(ILanguageRepository languageRepository, ILanguagesService languagesService, ILogger<LanguagesController> logger)
        {
            _languageRepository = languageRepository;
            _languagesService = languagesService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Language>> GetAll()
        {
            IEnumerable<Language> languages = _languagesService.getAvailableLanguages();
            if (languages.Any())
                return Ok(languages);

            _logger.LogError("Languages table is probably empty! Should contain values");
            return NotFound(new ApiError { ErrorMessage="No language found!", StatusCode=StatusCodes.Status404NotFound});
        }

        [HttpGet("{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Language>> GetByAny(string value)
        {
            Language? language = _languagesService.GetLanguage(value);
            if (language is not null)
                return Ok(language);

            return NotFound(new ApiError { ErrorMessage = $"No language with field value: `{value}` found! Please, check your data", StatusCode = StatusCodes.Status404NotFound });
        }
    }
}
