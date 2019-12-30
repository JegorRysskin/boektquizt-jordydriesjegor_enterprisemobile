using BoektQuiz.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class RegisterViewModelTests
    {
        private RegisterViewModel _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new RegisterViewModel();
        }

        [Test]
        public void EmptyUsernameAndPasswordShouldDisableRegisterTeamButton()
        {
            //Arrange
            _sut.Username = String.Empty;
            _sut.Password = String.Empty;

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void EmptyUsernameShouldDisableRegisterTeamButton()
        {
            //Arrange
            _sut.Username = String.Empty;
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void EmptyPasswordAndPasswordShouldDisableRegisterTeamButton()
        {
            //Arrange
            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = String.Empty;

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void ToShortPasswordAndPasswordShouldDisableRegisterTeamButton()
        {
            //Arrange
            _sut.Username = Guid.NewGuid().ToString().Substring(0, 2);
            _sut.Password = Guid.NewGuid().ToString().Substring(0, 5);

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void CorrectlyFilledInPasswordAndPasswordShouldEnableRegisterTeamButton()
        {
            //Arrange
            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(true));
        }
    }
}
