using HemtentaTdd2017.bank;
using Moq;
using Xunit;

namespace UnitTestEnvironment
{
    public class BankUnitTest
    {       
        IAccount account;
        //Mock<IAccount> accountMock = new Mock<IAccount>();

        //Account acnt = new IAccount { Deposit = 0, Withdraw = 0 };

        ////var test = new IAccount { Deposit = deposit, Withdraw = withdraw };

        public BankUnitTest()
        {
            IAccount account = new Account();
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

        //Withdraw
        [Fact]
        public void Withdraw_Withdraw_Is_Successful()
        {
            IAccount account = new Account();
            //Arrange
            //account = new Account(accountMock.Object);

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
            IAccount account = new Account();
            //Ska kasta exception
            //testade först om den kunde ta 0, då failar den. Då man inte kan ta ut 0 kr.
            //account = new Account(accountMock.Object);

            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(10));
        }

        [Fact]
        public void Withdraw_Throw_Exception_Wrong_Input()
        {
            IAccount account = new Account();
            //account = new Account(accountMock.Object);

            Assert.Throws<IllegalAmountException>(() => account.Withdraw(double.NegativeInfinity));
        }

        #endregion

        //TransferFunds
        [Fact]
        public void TransferFunds_Success()
        {
            //var destination = new Account();
            IAccount account = new Account();
            IAccount destination = new Account();

            destination.Deposit(250);

            //destination, amount
            destination.TransferFunds(account, 200);


            var result = account.Amount;
            var expected = 200;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TransferFunds_Failed_Cause_Wrong_Input()
        {
            //account = new Account(accountMock.Object);
            IAccount account = new Account();
            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(null, 0));
        }

        [Fact]
        public void TransferFunds_ThrowException_When_Not_Enough_Funds()
        {
            IAccount account = new Account();
            //account = new Account(accountMock.Object);

            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(account, 0));

        }

        [Fact]
        public void TransferFunds_IllegalAmountException()
        {
            IAccount account = new Account();
            //account = new Account(accountMock.Object);

            Assert.Throws<IllegalAmountException>(() => account.TransferFunds(account, double.NaN));

        }

    }
}
