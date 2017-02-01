namespace HemtentaTdd2017.bank
{
	// Representerar ett konto. Implementera den här!
	// Obs: i vanliga fall ska datatypen decimal användas
	// i stället för double när man hanterar pengar.
	public class Account : IAccount
	{
		double _amount;

		public Account()
		{
			
		}
		// behöver inte testas
		public double Amount
		{
			get
			{
				return _amount;
			}
		}

		// Sätter in ett belopp på kontot
		public void Deposit(double amount)
		{
			//Deposit amountet blir samma som amount.   
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

		// Överför ett belopp från ett konto till ett annat
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

		// Gör ett uttag från kontot
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
