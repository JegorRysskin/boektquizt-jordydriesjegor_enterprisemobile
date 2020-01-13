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
    public class LoginViewModelTests
    {
        private CrossConnectivityFake _crossConnectivityFake;
        private Mock<IBackendService> _backendServiceMock;
        private LoginViewModel _sut;


        [SetUp]
        public void SetUp()
        {
            _crossConnectivityFake = new CrossConnectivityFake();

            _backendServiceMock = new Mock<IBackendService>();
            
            _sut = new LoginViewModel(_backendServiceMock.Object);
            Connectivity.Instance = _crossConnectivityFake;
        }

        [Test]
        public void EmptyUsernameAndEmptyPassword_ShouldDisableLoginButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = String.Empty;
            _sut.Password = String.Empty;

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void EmptyUsernameAndPassword_ShouldDisableLoginButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = String.Empty;
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void UsernameAndEmptyPassword_ShouldDisableLoginButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = String.Empty;

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void TooShortPasswordAndPassword_ShouldDisableLoginButton()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = Guid.NewGuid().ToString().Substring(0, 2);
            _sut.Password = Guid.NewGuid().ToString().Substring(0, 5);

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void CorrectlyFilledInPasswordAndPassword_ShouldEnableLoginButton_IfConnectionIsEstablished()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(true));
        }

        [Test]
        public void CorrectlyFilledInPasswordAndPassword_ShouldDisableLoginButton_IfNoConnectionIsEstablished()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();

            //Act
            var canExecuteLoginCommand = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommand, Is.EqualTo(false));
        }

        [Test]
        public void LoginCommand_ChangeInConnectionShouldReEvaluateCanExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            _sut.Username = Guid.NewGuid().ToString();
            _sut.Password = Guid.NewGuid().ToString();
            var canExecuteLoginCommandBefore = _sut.LoginCommand.CanExecute(null);

            //Act
            _crossConnectivityFake.ConnectionValue = true;
            var canExecuteLoginCommandAfter = _sut.LoginCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteLoginCommandBefore, Is.Not.EqualTo(canExecuteLoginCommandAfter));
        }
    }
}
