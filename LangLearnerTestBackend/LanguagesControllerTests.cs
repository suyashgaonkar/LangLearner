using LangLearner.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using LangLearner.Database;
using LangLearner.Services;
using Microsoft.Extensions.Logging;
using LangLearner.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.IdentityModel.Tokens;
using LangLearner.Database.Repositories;
using AutoFixture;
using LangLearner.Models.Dtos.Responses;
using Newtonsoft.Json.Linq;
//using EntityFrameworkCore.Testing.NSubstitute;

namespace LangLearnerTestBackend
{
    public class LanguagesControllerTests
    {
        //[Fact]
        //public void GetLanguagesListFiveItems()
        //{
        //    //// Assign
        //    //var dbOptions = A.Fake<DbContextOptions<AppDbContext>>();
        //    //var context = new AppDbContext(dbOptions);
        //    //var languageService = A.Fake<LanguagesService>();
        //    //A.CallTo(() => languageService.getAvailableLanguages()).Returns(
        //    //    new List<Language> { 
        //    //        new Language { Name="English", Code="en", NativeName="English"},
        //    //        new Language { Name="Spanish", Code="es", NativeName="Español"}
        //    //    }
        //    //);
        //    //var logger = A.Fake<ILogger<LanguagesController>>();

        //    //// Act
        //    //var controller = new LanguagesController(context, languageService, logger);
        //    //var actionResult = controller.GetLanguages();

        //    //// Assert
        //    //var result = actionResult.Result as OkObjectResult;
        //    //var returnLanguages = result?.Value as IEnumerable<Language>;
        //    //Assert.Equal(2, returnLanguages?.Count());
        //    //var options = new DbContextOptions<AppDbContext>();
        //    //var mockedDbContext = EntityFrameworkCore.Testing.Moq.Create.MockedDbContextFor<AppDbContext>(options);



        //    //var languageService = A.Fake<LanguagesService>();
        //    //var context = new AppDbContext(options);
        //    //A.CallTo(() => languageService.getAvailableLanguages())
        //    //    .Returns(new List<Language> { new Language { Name = "English", Code = "en", NativeName = "English" } });
        //    //var logger = A.Fake<ILogger<LanguagesController>>();

        //    // Act
        //    //var controller = new LanguagesController(mockedDbContext, languageService, logger);
        //    //var result = controller.GetLanguages().Result as OkObjectResult;

        //    // Assert
        //    //Assert.NotNull(result);
        //    //Assert.Equal(200, result.StatusCode);

        //    //var languages = result.Value as IEnumerable<Language>;
        //    //Assert.NotNull(languages);
        //    //Assert.Single(languages);
        //    //Assert.Equal("English", languages.First().Name);
        //    //Assert.Equal("en", languages.First().Code);
        //    //Assert.Equal("English", languages.First().NativeName);

        //    //var dbContextMock = new Mock<DbContext>();
        //    //var service = new LanguagesService(dbContextMock);

        //    //var entities = 
        //}


        private readonly Mock<ILanguageRepository> _languageRepositoryMock;

        private readonly Mock<ILogger<LanguagesController>> _loggerMock;

        private readonly ILanguagesService _languagesService;

        private Fixture _fixture;

        public LanguagesControllerTests()
        {
            _fixture = new Fixture();
            _languageRepositoryMock = new Mock<ILanguageRepository>();
            _languagesService = new LanguagesService(_languageRepositoryMock.Object);
            _loggerMock = new Mock<ILogger<LanguagesController>>();
        }

        [Fact]
        public void GetLanguagesNotEmpty()
        {
            int numberLanguages = 3;
            var languageList = _fixture.CreateMany<Language>(numberLanguages).ToList();

            _languageRepositoryMock.Setup(repo => repo.GetAll()).Returns(languageList);

            var _controller = new LanguagesController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object);

            OkObjectResult? result = _controller?.GetAll().Result as OkObjectResult;

            Assert.NotNull(result);


            var languages = result.Value as IEnumerable<Language>;


            Assert.Equal(200, result?.StatusCode);
            Assert.Equal(numberLanguages, languages?.Count());

        }


        [Fact]
        public void GetLanguagesEmpty()
        {
            var languageList = _fixture.CreateMany<Language>(0).ToList();

            _languageRepositoryMock.Setup(repo => repo.GetAll()).Returns(languageList);

            var _controller = new LanguagesController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object);

            NotFoundObjectResult? result = _controller?.GetAll().Result as NotFoundObjectResult;

            var errorResponse = result?.Value as ApiError;

            Assert.Equal(404, result?.StatusCode);
            Assert.Equal("No language found!", errorResponse?.ErrorMessage);
            Assert.Equal(404, errorResponse?.StatusCode);

        }

        [Fact]
        public void GetLanguageByAnyExists()
        {
            var code = "en";
            var language = _fixture.Create<Language>();
            language.Code = code;

            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(code)).Returns(language);
            var _controller = new LanguagesController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object);

            OkObjectResult? result = _controller?.GetByAny(code).Result as OkObjectResult;

            var languageFromAPI = result?.Value as Language;

            Assert.Equal(200, result?.StatusCode);
            Assert.Equal(code, languageFromAPI?.Code);
        }

        [Fact]
        public void GetLanguageByAnyNotExists()
        {
            var dummyCode = "ens";

            _languageRepositoryMock.Setup(repo => repo.GetLanguageByAny(dummyCode)).Returns((Language?)null);
            var _controller = new LanguagesController(_languageRepositoryMock.Object, _languagesService, _loggerMock.Object);

            NotFoundObjectResult? result = _controller?.GetByAny(dummyCode).Result as NotFoundObjectResult;
            ApiError? languageFromAPI = result?.Value as ApiError;

            Assert.Equal(404, result?.StatusCode);
            Assert.Equal($"No language with field value: `{dummyCode}` found! Please, check your data", languageFromAPI?.ErrorMessage);
            Assert.Equal(404, languageFromAPI?.StatusCode);
        }
    }
}
