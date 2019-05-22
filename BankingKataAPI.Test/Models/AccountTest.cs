using BankingKataAPI.Models;
using Xunit;

namespace BankingKataAPI.Test.Models
{
    public class AccountTest
    {
        [Fact]
        public void CanSpecifyAStartingBalance()
        {
            var account = new Account(new Money(65.98m));

            Assert.Equal(account.Balance, new Money(65.98m));
        }

        [Fact]
        public void ShouldGenerateIdForAccount()
        {
            var account1 = new Account(new Money());
            var account2 = new Account(new Money());

            Assert.NotEqual(account1.Id, account2.Id);
        }
    }
}
