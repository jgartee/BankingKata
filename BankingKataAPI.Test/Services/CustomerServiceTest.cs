using System;
using System.Collections.Generic;
using System.Text;
using BankingKataAPI.Models;
using BankingKataAPI.Persistence;
using BankingKataAPI.Services;
using Microsoft.CodeAnalysis;
using Xunit;

namespace BankingKataAPI.Test.Services
{
    public class CustomerServiceTest
    {
        private readonly IRepository<User> _repository;
        private readonly CustomerService _service;
        private readonly User _user;
        private readonly Guid _userId;
        private readonly MockEmailService _emailService;

        public CustomerServiceTest()
        {
            _user = new User("example@email.com");
            _userId = _user.Id;

            _repository = new NetworkRepository<User>();
            _repository.Save(_user);

            _emailService = new MockEmailService();
            _service = new CustomerService(_repository, _emailService);
        }

        [Fact]
        public void UpdateEmailSavesTheNewAddress()
        {
            var expectedEmailAddress = "my-new-email@example.com";

            _service.UpdateEmailAddress(_userId, expectedEmailAddress);
            var updatedUser = _repository.FindOne(_userId);

            Assert.Equal(expectedEmailAddress, updatedUser.EmailAddress);
        }

        [Fact]
        public void UpdateEmailThrowsAnErrorForInvalidUserId()
        {
            var invalidUserId = Guid.NewGuid();

            var exception = Assert.Throws<Exception>(() => _service.UpdateEmailAddress(invalidUserId, "something@somewhere.com"));
            Assert.Equal("Invalid userId", exception.Message);
        }

        [Fact]
        public void UpdateEmailSavesACopyOfTheUserWithoutModifyingTheOriginal()
        {
            var expectedEmailAddress = "my-new-email@example.com";

            _service.UpdateEmailAddress(_userId, expectedEmailAddress);
            Assert.NotEqual(expectedEmailAddress, _user.EmailAddress);
        }

        [Fact(Skip = "Implement this feature")]
        public void SendFraudAlertShouldNotifyTheUserByEmail()
        {
            var expectedEmailAddress = "email@example.com";
            var expectedMessage = "You may have been the victim of fraud!";
            var expectedConfirmationCode = Guid.NewGuid();

            // TODO: use your mock here...

            var confirmationCode = _service.SendFraudAlert(_userId);

            Assert.Equal(expectedConfirmationCode, confirmationCode);
        }

        [Fact(Skip = "Implement this test")]
        public void SendFraudAlertWillThrowAnErrorIfUserIdIsNotFound()
        {

        }
    }

    public class MockEmailService : IEmailService
    {
        public Guid SendMessage(string emailAddress, string message)
        {
            return Guid.Empty;
        }
    }
}
