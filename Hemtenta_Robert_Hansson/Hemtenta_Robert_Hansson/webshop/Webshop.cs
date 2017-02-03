namespace HemtentaTdd2017.webshop
{
    //Har gjort en FakeKlass av billing i testmiljön istället för att Mocka. Då jag kände att jag behövde träningen och upplevde det
    //mer passande för uppgiften än dom andra. 

    //Saker jag hade gjort annorlunda:
    //Personligen tycker jag att Productklassen hade för lite properties. Finns endast namn att identifiera produkten på. Det hade jag gjort annorlunda.
    //Utöver det, hade jag gjort en dedikerad User klass, med saker som konto, historik osv.

    //Svaren på frågorna ligger i WebShopUnitTest.


    public class Webshop : IWebshop
    {
       
        public decimal _TotalCost { get; set; }
        public IBasket _basket { get; set; }
        public IBilling _iBilling { get; set; }

        public Webshop(IBasket _basket)
        {
            this._basket = _basket;
        }

        public IBasket Basket
        {
            get
            {
                return _basket;
            }
        }


        //Baskets totala kostnad ska till checkout. 
        //Sedan ska checkout ta Amount i billing MINUS total kostnad
        public void Checkout(IBilling billing)
        {
            //_TotalCost = _basket.TotalCost;
            _iBilling = billing;

            //Basket.TotalCost = _TotalCost;
            var _TotalCost = _basket.TotalCost;

            billing.Pay(_TotalCost);
            //_TotalCost = totalCost;
            
        } 
    }
}
