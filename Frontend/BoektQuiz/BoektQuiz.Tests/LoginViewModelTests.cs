using BoektQuiz.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        private LoginViewModel _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LoginViewModel();
        }

        [Test]
        public void EmptyTeamNameAndPasswordShouldDisableLoginButton()
        {
            //Arrange
            _sut.TeamName = String.Empty;
            _sut.Password = String.Empty;

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void EmptyTeamNameShouldDisableLoginButton()
        {
            //Arrange
            _sut.TeamName = String.Empty;
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
            _sut.TeamName = Guid.NewGuid().ToString();
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
            _sut.TeamName = Guid.NewGuid().ToString().Substring(0, 2);
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
            _sut.TeamName = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(true));
        }
    }
}
