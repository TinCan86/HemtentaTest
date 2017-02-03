using HemtentaTdd2017.bank;
using Xunit;

namespace UnitTestEnvironment
{
    public class BankUnitTest
    {       
        public BankUnitTest()
        {
            IAccount account = new Account();           
        }

        #region Deposit

        [Fact]
        public void Deposit_If_Deposit_Is_Successful()
        {
            IAccount account = new Account();

            account.Deposit(25);

            var expected = 25;
            var result = account.Amount;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Deposit_If_Deposit_Dont_Go_Through_ThrowExeption()
        {
            IAccount account = new Account();

            Assert.Throws<InsufficientFundsException>(() => account.Deposit(-10));   
        }

        [Fact]
        public void Deposit_Throw_Exception_If_NaN_Or_NegativeInfitive()
        {
            IAccount account = new Account();

            Assert.Throws<IllegalAmountException>(() => account.Deposit(double.NaN));
        }

        #endregion

        #region Withdraw tester

        [Fact]
        public void Withdraw_Withdraw_Is_Successful()
        {
            IAccount account = new Account();
            
            account.Deposit(25);
            account.Withdraw(22);

            var expected = 3;
            var result = account.Amount;

            Assert.Equal(expected, result);
        }
        [Fact]
        public void Withdraw_If_Withdraw_Dont_Go_Through__Fails_ThrowExeption() 
        {
            IAccount account = new Account();
            //Ska kasta exception
            //testade först om den kunde ta 0, då failar den. Då man inte kan ta ut 0 kr.

            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(10));
        }

        [Fact]
        public void Withdraw_Throw_Exception_Wrong_Input()
        {
            IAccount account = new Account();

            Assert.Throws<IllegalAmountException>(() => account.Withdraw(double.NegativeInfinity));
        }

        #endregion

        [Fact]
        public void TransferFunds_Success()
        {
            IAccount account = new Account();
            IAccount destination = new Account();

            destination.Deposit(250);
            destination.TransferFunds(account, 200);

            var result = account.Amount;
            var expected = 200;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TransferFunds_Failed_Cause_Wrong_Input()
        {
            IAccount account = new Account();
            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(null, 0));
        }

        [Fact]
        public void TransferFunds_ThrowException_When_Not_Enough_Funds()
        {
            IAccount account = new Account();

            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(account, 0));
        }

        [Fact]
        public void TransferFunds_IllegalAmountException()
        {
            IAccount account = new Account();

            Assert.Throws<IllegalAmountException>(() => account.TransferFunds(account, double.NaN));
        }

    }
}
