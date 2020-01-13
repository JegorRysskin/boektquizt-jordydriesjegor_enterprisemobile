using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class RegisterViewModelTests
    {
        private CrossConnectivityFake _crossConnectivityFake;
        private Mock<IBackendService> _backendServiceMock;
        private RegisterViewModel _sut;

        [SetUp]
        public void SetUp()
        {
            _crossConnectivityFake = new CrossConnectivityFake();

            _backendServiceMock = new Mock<IBackendService>();

            _sut = new RegisterViewModel(_backendServiceMock.Object);
            Connectivity.Instance = _crossConnectivityFake;
        }

        [Test]
        public void EmptyUsernameAndEmptyPassword_ShouldDisableRegisterTeamButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = String.Empty;
            _sut.Password = String.Empty;

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void EmptyUsernameAndPassword_ShouldDisableRegisterTeamButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = String.Empty;
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void UsernameAndEmptyPassword_ShouldDisableRegisterTeamButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = String.Empty;

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void TooShortPasswordAndPassword_ShouldDisableRegisterTeamButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = Guid.NewGuid().ToString().Substring(0, 2);
            _sut.Password = Guid.NewGuid().ToString().Substring(0, 5);

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void CorrectlyFilledInPasswordAndPassword_ShouldEnableRegisterTeamButton_IfConnectionIsEstablished()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(true));
        }

        [Test]
        public void CorrectlyFilledInPasswordAndPassword_ShouldDisableRegisterTeamButton_IfNoConnectionIsEstablished()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteRegisterTeamCommand = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteRegisterTeamCommand, Is.EqualTo(false));
        }

        [Test]
        public void RegisterTeamCommand_ChangeInConnectionShouldReEvaluateCanExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();
            var canExecuteLoginCommandBefore = _sut.RegisterTeamCommand.CanExecute(null);

            //Act
            _crossConnectivityFake.ConnectionValue = true;
            var canExecuteLoginCommandAfter = _sut.RegisterTeamCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommandBefore, Is.Not.EqualTo(canExecuteLoginCommandAfter));
        }
    }
}
