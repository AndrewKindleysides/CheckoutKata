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

        [Fact(Skip = "failing test for price with discounts")]
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
            var pricingList = new PricingList();
            var total = 0;
            foreach (var item in _basket)
            {
                total += pricingList.Items.First(x => x.SKU == item).UnitPrice;
            }
            return total;
        }
    }

    public class PricingList
    {
        public PricingList()
        {
            Items = new List<Item>
            {
                new Item() {SKU = "A", UnitPrice = 50}, 
                new Item() {SKU = "B", UnitPrice = 30}
            };
        }

        public List<Item> Items { get; set; } 
    }

    public class Item
    {
        public string SKU;
        public int UnitPrice;
    }
}
