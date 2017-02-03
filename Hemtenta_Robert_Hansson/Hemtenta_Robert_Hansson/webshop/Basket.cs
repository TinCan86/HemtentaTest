using System;

namespace HemtentaTdd2017.webshop
{
    public class Basket : IBasket
    {
        //IBasket basket;
        //List<Product> productList = new List<Product>();
        Product product;
        int amountOfProducts;
      

        public decimal TotalCost
        {
            get
            {   
                return product.Price * amountOfProducts;
            }
        }

        public void AddProduct(Product p, int amount)
        {
            if (p == null || amount <= 0)
            {
                throw new Exception();
            }

            product = p;
            amountOfProducts += amount; 
        }

        public void RemoveProduct(Product p, int amount)
        {
            if (p == null || amount <= 0)
            {
                throw new NothingToRemoveException(); 
            }

            if (amount > amountOfProducts)
            {
                throw new NothingToRemoveException();
            }

            amountOfProducts -= amount;
            product = p;
        }
    }
}
