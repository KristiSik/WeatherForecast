using DataLayer;
using DataLayer.Models;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using WeatherForecast.Controllers;
using WeatherForecast.Tests.Fake;

namespace WeatherForecast.Tests
{
    [TestFixture]
    public class ProfileControllerTests
    {
        private Mock<UnitOfWork> mock;

        private ProfileController controller;
        public ProfileControllerTests()
        {
            mock = new Mock<UnitOfWork>();
            controller = new ProfileController(mock.Object);
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
        [TestCase("1dqwdw", "1vsasav")]
        [TestCase("1asdasd", "")]
        [TestCase("213123123", "")]
        [TestCase("", "")]
        public void Login_When_input_wrong_login_and_password_Then_viewbag_error(string login, string password)
        {
            controller.Login(new Models.LoginUser() { Login = login, Password = password });
            string expected = "Wrong Login or Password";
            Assert.AreEqual(expected, controller.ViewBag.Error);
        }
        [Test]
        public void 
    }
}