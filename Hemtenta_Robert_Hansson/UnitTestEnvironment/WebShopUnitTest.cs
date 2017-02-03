using HemtentaTdd2017.webshop;
using Moq;
using System;
using Xunit;

namespace UnitTestEnvironment
{
    //Svar på frågor: 
    //1. Vilka metoder och properties behöver testas? 
    //Metoderna och Properties som bör testas Checkout, AddProduct, RemoveProduct, Pay, TotalCost och Balance.
    //2. Ska några exceptions kastas?
    //Ja, IllegalProductException tex, om produkten är null eller har negativt tal. Men har lagt till custom exceptions: NothingToRemoveException, InsufficientFundsException, NegativFundsException.
    //3. Vilka är domänerna för IWebshop och IBasket?
    //IWebshop: Där finns Billing, som har datatypen object. Object kan vara object eller null.
    //IBasket: Det är P och Amount. Dessa har datatyper object och Int. Object kan vara object eller null. Medans int kan vara heltal mellan det största och minsta tillåtna värdet för respektive datatyp


    public class WebShopUnitTest
    {

        Mock<IBilling> billingMock;


        public WebShopUnitTest()
        {
            IBasket basket = new Basket();
            billingMock = new Mock<IBilling>();
        }

        #region AddProduct Section

        [Fact]
        public void AddProduct_To_Basket_Success()
        {
            Product thingProduct = new Product { Name = "Boll", Price = 20 };           
            IBasket basket = new Basket();

            basket.AddProduct(thingProduct, 5);

            var expected = 100;
            var result = basket.TotalCost;
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddProduct_Throw_Exception_If_Amount_Is_Zero_Or_Negative()
        {
            Product productThing = new Product { Name = "Sak", Price = 30 };
            IBasket basket = new Basket();

            var amount = -1;

            Assert.Throws<Exception>(() => basket.AddProduct(productThing, amount));
        }

        [Fact]
        public void AddProduct_Throw_Exception_If_Object_Is_Null()
        {            
            IBasket basket = new Basket();

            var amount = 1;           

            Assert.Throws<Exception>(() => basket.AddProduct(null, amount));
        }

        #endregion

        #region RemoveProduct Section
        
        [Fact]
        public void RemoveProduct_Throw_Exception_If_Object_Is_Null()
        {
            IBasket basket = new Basket();

            var amount = 1;

            Assert.Throws<NothingToRemoveException>(() => basket.RemoveProduct(null, amount));
        }

        [Fact]
        public void RemoveProduct_Throw_Exception_If_There_Is_No_More_To_Remove()
        {
            Product productThing = new Product { Name = "Sak", Price = 30 };
            IBasket basket = new Basket();

            var amount = 0;

            Assert.Throws<NothingToRemoveException>(() => basket.RemoveProduct(productThing, amount));
        }

        [Fact]
        public void RemoveProduct_Success_On_Removing_Product()
        {
            Product thingProduct = new Product { Name = "Boll", Price = 20 };
            IBasket basket = new Basket();

            //Lägger till 2 produkter, 2x20kr = 40. 
            basket.AddProduct(thingProduct, 2);
            basket.RemoveProduct(thingProduct, 1);

            //Eftersom jag tar bort 1 produkt, så ska totalkostnaden ligga på 20kr.
            var expected = 20;
            var result = basket.TotalCost;

            Assert.Equal(expected, result);
        }

        #endregion

        [Fact]
        public void Balance_CheckBalance()
        {
            IBilling billing = new FakeBilling();

            billing.Balance = 200;

            var expected = 200;
            var result = billing.Balance;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Pay_Success_On_Paying()
        {
            IBilling billing = new FakeBilling();

            billing.Balance = 400;
            billing.Pay(300);

            //Eftersom vi har 400 på kontot och betalar 300, så ska det vara 100 kvar.
            var expected = 100;
            var result = billing.Balance;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Pay_Throw_If_Funds_Isnt_Enough()
        {
            IBilling billing = new FakeBilling();

            billing.Balance = 400;

            //Eftersom vi har 400 på kontot och betalar 500, så ska det kastas ett exception.
            Assert.Throws<InsufficientFundsException>(() => billing.Pay(500));
        }

        [Fact]
        public void Pay_Throw_If_Trying_To_Pay_With_Minus_Amount()
        {
            IBilling billing = new FakeBilling();
            
            Assert.Throws<NegativFundsException>(() => billing.Pay(-500));
        }

        [Fact]
        public void Checkout_Success()
        {
            Product thingProduct = new Product { Name = "Boll", Price = 50 };
            IBasket basket = new Basket();
            IWebshop webshop = new Webshop(basket);
            IBilling billing = new FakeBilling();

            billing.Balance = 1000;

            basket.AddProduct(thingProduct, 4);
            var totalCost = basket.TotalCost;

            webshop.Checkout(billing);

            //Eftersom vi har 1000 på kontot och betalar 200, så ska det vara 800 kvar.
            var expected = 800;
            var result = billing.Balance;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Checkout_Not_Enough_Funds()
        {
            IBasket basket = new Basket();
            IWebshop webshop = new Webshop(basket);
            IBilling billing = new FakeBilling();

            Product thingProduct = new Product { Name = "Boll", Price = 50 };

            billing.Balance = 100;

            basket.AddProduct(thingProduct, 4);
            var totalCost = basket.TotalCost;
           
            Assert.Throws<InsufficientFundsException>(() => webshop.Checkout(billing));

        }

        [Fact]
        public void Checkout_Throw_Negative()
        {
            IBasket basket = new Basket();
            IWebshop webshop = new Webshop(basket);
            IBilling billing = new FakeBilling();

            Product thingProduct = new Product { Name = "Boll", Price = -50 };

            billing.Balance = 100;

            basket.AddProduct(thingProduct, 4);
            var totalCost = basket.TotalCost;

            //Eftersom beloppet som ska betalas är på minusbelopp, så blir det error,
            //då man inte ska kunna skicka negativa fakturor.
            Assert.Throws<NegativFundsException>(() => webshop.Checkout(billing));

        }
    }
}
