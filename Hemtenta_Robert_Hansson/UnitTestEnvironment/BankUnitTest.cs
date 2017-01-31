using HemtentaTdd2017.bank;
using Moq;
using Xunit;

namespace UnitTestEnvironment
{
    public class BankUnitTest
    {       
        //Account account;
        IAccount account;
        Mock<IAccount> accountMock = new Mock<IAccount>();

        //Account acnt = new IAccount { Deposit = 0, Withdraw = 0 };

        ////var test = new IAccount { Deposit = deposit, Withdraw = withdraw };

        public BankUnitTest()
        {

            //var dataSource = new Mock<IAccount>();
            //dataSource.Setup(m => m.TransferFunds(It.IsAny<IAccount>())).Returns();
            //accountMock = 
            //account = new Account(accountMock.Object);

            //    ////accountMock.Setup(x => x.Amount()).Get(accountMock);
            //    ////Sätter upp mocken att leta rätt på en user vid name username i databasen och returnerar username.
            //    ////acountMock.Setup(x => x.(username)).Returns(new User(username));
            //    //var account_Robert = accountMock.Object;
            //    //var account_Calle = accountMock.Object;

            //    //accountMock = new Account(account_Robert);
            //    //accountMock = new Account(account_Calle);

        }



        #region Deposit

        // Deposit, så sätter in ett belopp på kontot
        [Fact]
        public void Deposit_If_Deposit_Is_Successful()
        {
            account = new Account(accountMock.Object);

            account.Deposit(25);

            var expected = 25;
            var result = account.Amount;

            Assert.Equal(expected, result);
        }

        //[Fact]
        //public void Deposit_If_Deposit_Fails()
        //{
        //    account = new Account(accountMock.Object);

        //    account.Deposit(20);

        //    var expected = 5;
        //    var result = account.Amount;

        //    Assert.Equal(expected, result);
        //}

        [Fact]
        public void Deposit_If_Deposit_Dont_Go_Through_ThrowExeption()
        {
            account = new Account(accountMock.Object);
            
            Assert.Throws<InsufficientFundsException>(() => account.Deposit(-10));

            
        }

        [Fact]
        public void Deposit_Throw_Exception_If_NaN_Or_NegativeInfitive()
        {
            account = new Account(accountMock.Object);

            Assert.Throws<IllegalAmountException>(() => account.Deposit(double.NaN));
        }

        #endregion

        #region Withdraw tester

        //Withdraw
        [Fact]
        public void Withdraw_Withdraw_Is_Successful()
        {
            //Arrange
            account = new Account(accountMock.Object);

            //Act
            account.Deposit(25);
            account.Withdraw(22);

            var expected = 3;
            var result = account.Amount;

            //Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Withdraw_If_Withdraw_Dont_Go_Through__Fails_ThrowExeption() 
        {
            //Ska kasta exception
            //testade först om den kunde ta 0, då failar den. Då man inte kan ta ut 0 kr.
            account = new Account(accountMock.Object);

            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(10));
        }

        [Fact]
        public void Withdraw_Throw_Exception_Wrong_Input()
        {
            account = new Account(accountMock.Object);

            Assert.Throws<IllegalAmountException>(() => account.Withdraw(double.NegativeInfinity));
        }

        #endregion

        //TransferFunds
        [Theory]
        public void TransferFunds_Success(IAccount destination, double amount)
        {
            account = new Account(accountMock.Object);
            destination = new Account(accountMock.Object);

            destination.Deposit(250);

            //destination, amount
            destination.TransferFunds(account, 200);

            var result = account.Amount;
            var expected = 100;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TransferFunds_Failed_Cause_Wrong_Input()
        {
            account = new Account(accountMock.Object);

            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(null, 0));
        }

        [Fact]
        public void TransferFunds_ThrowException_When_Not_Enough_Funds()
        {
            account = new Account(accountMock.Object);

            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(account, 0));

        }

        [Fact]
        public void TransferFunds_IllegalAmountException()
        {
            account = new Account(accountMock.Object);

            Assert.Throws<IllegalAmountException>(() => account.TransferFunds(account, double.NaN));

        }

    }
}
