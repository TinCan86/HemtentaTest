using System;

namespace HemtentaTdd2017.webshop
{
    // Testa och implementera
    public interface IBasket
    {
        void AddProduct(Product p, int amount);
        void RemoveProduct(Product p, int amount);
        decimal TotalCost { get; }
    }

    public class NothingToRemoveException : Exception { }
    public class InsufficientFundsException : Exception { }
    public class NegativFundsException : Exception { }
}
