using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class Basket : IBasket
    {
        IBasket basket;
        List<Product> productList = new List<Product>();
        Product product;
        int amountOfProducts;


        public Basket()
        {
            basket = new Basket();
        }

        public decimal TotalCost
        {
            get
            {   
                return product.Price * amountOfProducts;
            }
        }

        public void AddProduct(Product p, int amount)
        {
            product = p;
            amountOfProducts = amount; 
            //basket.AddProduct(p, 4);
        }

        public void RemoveProduct(Product p, int amount)
        {
            productList.Remove(p);      
            //basket.RemoveProduct(p, 1);
        }
    }
}
