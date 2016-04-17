using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CheckoutKata
{
    public class CheckoutKataTests
    {
        private readonly Checkout _checkout;

        public CheckoutKataTests()
        {
            _checkout = new Checkout(new PricingList());
        }

        [Fact]
        public void Checkout_can_scan_an_item()
        {
            _checkout.Scan("A");
            Assert.Equal(1, _checkout.ItemsInBasket);
        }

        [Fact]
        public void Checkout_can_scan_multiple_items()
        {

            _checkout.Scan("A");
            _checkout.Scan("B");
            Assert.Equal(2, _checkout.ItemsInBasket);
        }

        [Fact]
        public void Checkout_calculates_the_total_price()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            Assert.Equal(80, _checkout.GetTotalPrice());
        }

        [Fact]
        public void Checkout_calculates_total_price_with_discounts()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B");
            Assert.Equal(160, _checkout.GetTotalPrice());
        }

        [Fact]
        public void Checkout_calculates_total_price_with_multiple_discounts()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("B");
            Assert.Equal(175, _checkout.GetTotalPrice());
        }

    }

    public class Checkout
    {
        private readonly List<string> _basket;
        private readonly PricingList _pricingList;

        public Checkout(PricingList pricingList)
        {
            _pricingList = pricingList;
            _basket = new List<string>();
        }

        public void Scan(string s)
        {
            _basket.Add(s);
        }

        public int ItemsInBasket
        {
            get { return _basket.Count; }
        }

        public int GetTotalPrice()
        {
            var total = 0;
            foreach (var item in _basket)
            {
                total += _pricingList.Items.First(x => x.SKU == item).UnitPrice;
            }

            var discountTotal = ApplyDiscounts(_pricingList);
            return total - discountTotal;
        }

        private int ApplyDiscounts(PricingList pricingList)
        {
            var discountTotal = 0;
            if (_basket.FindAll(e => e == "A").Count > 0)
            {

                var itemA = pricingList.Items.First(x => x.SKU == "A");
                if (_basket.FindAll(e => e == "A").Count >= itemA.SpecialPricing.Quantity)
                {
                    var originalPrice = itemA.UnitPrice*itemA.SpecialPricing.Quantity;
                    discountTotal += originalPrice - itemA.SpecialPricing.Price;
                }
            }

            if (_basket.FindAll(e => e == "B").Count > 0)
            {
                var itemB = pricingList.Items.First(x => x.SKU == "B");
                if (_basket.FindAll(e => e == "B").Count >= itemB.SpecialPricing.Quantity)
                {
                    var originalPrice = itemB.UnitPrice * itemB.SpecialPricing.Quantity;
                    discountTotal += originalPrice - itemB.SpecialPricing.Price;
                }
            }
            return discountTotal;
        }
    }

    public class PricingList
    {
        public PricingList()
        {
            Items = new List<Item>
            {
                new Item() {SKU = "A", UnitPrice = 50, SpecialPricing = new SpecialPricing(){Quantity = 3, Price = 130}}, 
                new Item() {SKU = "B", UnitPrice = 30, SpecialPricing = new SpecialPricing(){Quantity = 2, Price = 45}}
            };
        }

        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string SKU;
        public int UnitPrice;
        public SpecialPricing SpecialPricing;
    }

    public class SpecialPricing
    {
        public int Quantity;
        public int Price;
    }
}
