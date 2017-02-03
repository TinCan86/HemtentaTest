namespace HemtentaTdd2017.bank
{
    public class Account : IAccount
	{
		double _amount;

		public Account()
		{
			
		}
		public double Amount
		{
			get
			{
				return _amount;
			}
		}

		public void Deposit(double amount)
		{
			if(amount <= 0)
			{
				throw new InsufficientFundsException();
			}

			if (double.IsNaN(amount) || double.IsNegativeInfinity(amount) || double.IsPositiveInfinity(amount))
			{
				throw new IllegalAmountException();
			}

			_amount = amount;
		}

		public void TransferFunds(IAccount destination, double amount)
		{
			if (destination == null || amount <= 0)
			{
				throw new OperationNotPermittedException();
			}

			if (double.IsNaN(amount) || double.IsNegativeInfinity(amount) || double.IsPositiveInfinity(amount))
			{
				throw new IllegalAmountException();
			}

            if (_amount >= amount)
            {
                destination.Deposit(amount);
                _amount -= amount;
            }

            else if (amount > _amount)
            {
                throw new InsufficientFundsException();
            }
		}

		public void Withdraw(double amount)
		{
			//Om uttaget från amount är större än vad som finns sparat i amount. så kastas ett exception
			//att det saknas pengar på kontot. Hanterar minus belopp
			if (amount > _amount)
			{
				throw new InsufficientFundsException();
			}

			//Kolla så det inte är en NaN not a number
			if (double.IsNaN(amount) || double.IsPositiveInfinity(amount) || double.IsNegativeInfinity(amount))
			{
				throw new IllegalAmountException();
			}

			//Annars så tar man Withdraw beloppet och tar minus på amount beloppet.            
			_amount -= amount;    
		}
	}
}
