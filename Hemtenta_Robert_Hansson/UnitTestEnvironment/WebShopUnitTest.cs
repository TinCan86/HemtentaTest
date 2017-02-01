using System;
using Xunit;
using Moq;
using HemtentaTdd2017.webshop;

namespace UnitTestEnvironment
{
    public class WebShopUnitTest
    {
        IBasket basket;

        public WebShopUnitTest()
        {
            IBasket basket = new Basket();
        }

        [Fact]
        public void AddProduct_To_Basket()
        {
            Product thingProduct = new Product { Price = 20 };
            IBasket basket = new Basket();

            basket.AddProduct(thingProduct, 5);

            //account.Deposit(25);

            var expected = 100;
            //var result = account.Amount;

            Assert.Equal(expected, basket.TotalCost);           

        }
    }
}
