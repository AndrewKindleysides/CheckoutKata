using System.Collections.Generic;
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

        [Fact(Skip="Checking test fails first")]
        public void Checkout_can_scan_multiple_items()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.Equal(1, checkout.ItemsInBasket);
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

    }
}
