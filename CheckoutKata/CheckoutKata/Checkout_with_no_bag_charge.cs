using Xunit;

namespace CheckoutKata
{
    public class Checkout_with_no_bag_charge
    {
        private readonly Checkout _checkout;

        public Checkout_with_no_bag_charge()
        {
            _checkout = new Checkout(new PricingList(), new NoBagChargeProvider(), 5);
        }

        [Fact]
        public void Empty_basket_means_checkout_has_zero_value()
        {
            Assert.Equal(0, _checkout.GetTotalPrice());
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

        [Fact]
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
            Assert.Equal(380, _checkout.GetTotalPrice());
        }

        [Fact]
        public void Checkout_calculates_total_price_with_multiple_items_including_those_with_no_special_offer()
        {
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("D");
            
            Assert.Equal(115, _checkout.GetTotalPrice());
        }

        [Fact]
        public void Checkout_calculates_total_price_with_multiple_discount_items_and_items_with_no_special_offers()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("B");
            _checkout.Scan("C");
            _checkout.Scan("D");

            Assert.Equal(210, _checkout.GetTotalPrice());
        }

        
    }

    public class NoBagChargeProvider : IBagChargeProvider
    {
        public int GetBagCharge(int itemsInBasket, int maxItemsPerBag)
        {
            return 0;
        }
    }

    public class Checkout_in_Scotland_adds_bag_charge
    {
        private readonly Checkout _checkout;


        public Checkout_in_Scotland_adds_bag_charge()
        {
            _checkout = new Checkout(new PricingList(), new ScottishBagChargeProvider(), 5);
        }

        [Fact]
        public void Checkout_adds_on_bag_charge_with_less_than_5_items()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            Assert.Equal(135, _checkout.GetTotalPrice());
        }

        [Fact]
        public void Checkout_adds_on_bag_charge_with_more_than_5_items()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");

            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            
            _checkout.Scan("A");
            _checkout.Scan("A");
            Assert.Equal(505, _checkout.GetTotalPrice());
        }
    }
}
