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

        [Fact]
        public void Checkout_calculates_total_price_with_multiple_discounts_and_singular_items()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("B");
            _checkout.Scan("B");
            Assert.Equal(255, _checkout.GetTotalPrice());
        }

        [Fact(Skip = "initial test for multiple discounts of the same item and singular items")]
        public void Checkout_calculates_total_price_with_multiple_discounts_of_the_same_item_and_singular_items()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            
            _checkout.Scan("B");
            _checkout.Scan("B");
            
            _checkout.Scan("B");
            _checkout.Scan("B");

            _checkout.Scan("B");
            Assert.Equal(390, _checkout.GetTotalPrice());
        }

    }
}
