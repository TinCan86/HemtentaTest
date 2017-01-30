using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    // Representerar ett konto. Implementera den här!
    // Obs: i vanliga fall ska datatypen decimal användas
    // i stället för double när man hanterar pengar.
    public class Account : IAccount
    {
        // behöver inte testas
        public double Amount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        // Sätter in ett belopp på kontot
        public void Deposit(double amount)
        {
            throw new NotImplementedException();
        }

        // Överför ett belopp från ett konto till ett annat
        public void TransferFunds(IAccount destination, double amount)
        {
            throw new NotImplementedException();
        }

        // Gör ett uttag från kontot
        public void Withdraw(double amount)
        {
            throw new NotImplementedException();
        }
    }
}
