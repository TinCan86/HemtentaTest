namespace HemtentaTdd2017.webshop
{
    public class FakeBilling : IBilling
    {
        //Fakeklass, står mer om varför i BankUnitTest
        decimal _amount; 

        public FakeBilling()
        {

        }

        public decimal Balance
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
            }
        }

        public void Pay(decimal amount)
        {
            if (amount > _amount)
            {
                throw new InsufficientFundsException();
            }
            if (amount <= 0)
            {
                throw new NegativFundsException();
            }

            _amount -= amount;
        }
    }
}
