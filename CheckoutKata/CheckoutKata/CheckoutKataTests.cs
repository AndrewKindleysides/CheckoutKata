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
    }

    public class Checkout
    {
        public void Scan(string s)
        {
            var basket = new List<string>();
            basket.Add(s);
            ItemsInBasket++;
        }

        public int ItemsInBasket { get; set; }
    }
}
