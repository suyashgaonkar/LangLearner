//using AutoFixture;
//using AutoMapper;
//using LangLearner;
//using LangLearner.Controllers;
//using LangLearner.Database.Repositories;
//using LangLearner.Exceptions;
//using LangLearner.Models.Auth;
//using LangLearner.Models.Dtos.Requests;
//using LangLearner.Models.Dtos.Responses;
//using LangLearner.Models.Entities;
//using LangLearner.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace testBackend.controllersTests
//{
//    public class UserControllerTests
//    {
//        private readonly IFixture _fixture;
//        private readonly Mock<IUserRepository> _userRepositoryMock;
//        private readonly Mock<ILanguageRepository> _languageRepositoryMock;

//        private readonly Mock<ILogger<UserController>> _loggerMock;
//        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
//        private readonly IUserService _userService;
//        private readonly Mock<IIdentityService> _identityServiceMock;
//        private readonly IMapper _mapper;

//        public UserControllerTests()
//        {
//            var mockMapper = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new AutoMapperProfile());
//            });
//            _mapper = mockMapper.CreateMapper();
//            _fixture = new Fixture();
//            _fixture.Customize<Language>(composer =>
//                composer
//                    .Without(l => l.NativeLanguageUsers)
//                    .Without(l => l.AppLanguageUsers)
//            );
//            _userRepositoryMock = new Mock<IUserRepository>();
//            _identityServiceMock = new Mock<IIdentityService>();
//            _languageRepositoryMock = new Mock<ILanguageRepository>();
//            _userService = new UserService(_userRepositoryMock.Object, _languageRepositoryMock.Object, _passwordHasher, _mapper, _identityServiceMock.Object);
//            _loggerMock = new Mock<ILogger<UserController>>();
//        }

//        [Fact]
//        public void RegisterUserWithoutPasswordException()
//        {
//            // Arrange
//            string email = "testUser@mail.com";
//            string username = "testUserName";
//            string tokenMock = "someToken";
//            string nativeLanguage = "english";
//            var correctLanguage = new Language() { };
//            var newUserDto = new CreateUserDto()
//            { Email = email, NativeLanguageName = nativeLanguage, Username = username };

//            var userController = new UserController(userRepository: _userRepositoryMock.Object,
//                userService: _userService,
//                mapper: _mapper,
//                logger: _loggerMock.Object);

//            // Act and Assert
//            var exception = Assert.Throws<APIValidationException>(() => userController.Register(newUserDto));
//            Assert.Equal("Some fields are missing or invalid!", exception.Message);
//            Assert.Equal(400, exception.StatusCode);
//            Assert.NotNull(exception.Errors);
//            Assert.True(exception.Errors.ContainsKey("Password"));
//            Assert.Contains("The Password field is required.", exception.Errors["Password"]);
//        }
//    }
//}
