using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace CheckoutKata
{
    public class CheckoutKataTests
    {
        [Fact]
        public void Checkout_can_scan_an_item()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            Assert.Equal(1, checkout.ItemsInBasket);
        }

        [Fact]
        public void Checkout_can_scan_multiple_items()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.Equal(2, checkout.ItemsInBasket);
        }

        [Fact]
        public void Checkout_calculates_the_total_price()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.Equal(80, checkout.GetTotalPrice());
        }

        [Fact]
        public void Checkout_calculates_total_price_with_discounts()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.Equal(160, checkout.GetTotalPrice());
        }
    }

    public class Checkout
    {
        private readonly List<string> _basket;
        private PricingList _pricingList = new PricingList();

        public Checkout()
        {
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
            return discountTotal;
        }
    }

    public class PricingList
    {
        public PricingList()
        {
            Items = new List<Item>
            {
                new Item() {SKU = "A", UnitPrice = 50, SpecialPricing = new SpecialPricing(){Quantity = 3,Price = 130}}, 
                new Item() {SKU = "B", UnitPrice = 30}
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
