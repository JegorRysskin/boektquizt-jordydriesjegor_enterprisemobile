using BoektQuiz.Services;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        private Mock<IBackendService> _backendServiceMock;
        private LoginViewModel _sut;

        [SetUp]
        public void SetUp()
        {
            _backendServiceMock = new Mock<IBackendService>();
            _sut = new LoginViewModel(_backendServiceMock.Object);
        }

        [Test]
        public void EmptyUsernameAndPasswordShouldDisableLoginButton()
        {
            //Arrange
            _sut.Username = String.Empty;
            _sut.Password = String.Empty;

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void EmptyUsernameShouldDisableLoginButton()
        {
            //Arrange
            _sut.Username = String.Empty;
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void EmptyPasswordAndPasswordShouldDisableLoginButton()
        {
            //Arrange
            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = String.Empty;

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void ToShortPasswordAndPasswordShouldDisableLoginButton()
        {
            //Arrange
            _sut.Username = Guid.NewGuid().ToString().Substring(0, 2);
            _sut.Password = Guid.NewGuid().ToString().Substring(0, 5);

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void CorrectlyFilledInPasswordAndPasswordShouldEnableLoginButton()
        {
            //Arrange
            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(true));
        }
    }
}
