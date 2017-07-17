using DataLayer.Models;
using Moq;
using NUnit.Framework;
using System.Data.Entity.Validation;
using WeatherForecast.Controllers;
using WeatherForecast.Services;

namespace WeatherForecast.Tests
{
    [TestFixture]
    public class ProfileControllerTests
    {
        private Mock<ILogger> logger;
        private Mock<IUserAccount> userAccount;
        private Mock<DataLayer.IUnitOfWork> uow;
        private ProfileController controller;
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger>();
            userAccount = new Mock<IUserAccount>();
            uow = new Mock<DataLayer.IUnitOfWork>();
            controller = new ProfileController(logger.Object, uow.Object, userAccount.Object);
        }
        [Test]
        [TestCase("1")]
        [TestCase("Bla")]
        [TestCase("")]
        public void Registration_When_password_less_than_6_symbols_Then_not_valid(string password)
        {
            User user = new User()
            {
                Firstname = "Michael",
                Lastname = "Jackson",
                Login = "Lollolol",
                Password = password,
                ConfirmPassword = password
            };
            var exception = Assert.Catch(() => {
                controller.Registration(user);
            });
            Assert.IsInstanceOf<DbEntityValidationException>(exception);
        }

        [Test]
        [TestCase("1", "1")]
        [TestCase("1123", "savasv1")]
        [TestCase("Login", "password")]
        [TestCase("1asdasd", "")]
        [TestCase("213123123", "")]
        [TestCase("", "")]
        public void Login_When_input_wrong_login_and_password_Then_viewbag_error(string login, string password)
        {
            userAccount.Setup(m => m.Login(new Models.LoginUser() { Login = login, Password = password }));
        }
        [Test]
        public void Registration_When_register_new_account_Then_ILogger_called()
        {
            User user = new User()
            {
                Firstname = "Michael",
                Lastname = "Jackson",
                Login = "1234567",
                Password = "1234567",
                ConfirmPassword = "1234567"
            };
            controller.Registration(user);
            logger.Verify(m => m.Log(LogLevel.Error, It.IsAny<string>()), Times.Once);
        }
    }
}