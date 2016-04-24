using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly List<string> _basket;
        private readonly PricingList _pricingList;
        private readonly IBagChargeProvider _bagChargeProvider;
        private readonly int _maxItemsPerBag;

        public Checkout(PricingList pricingList, IBagChargeProvider bagChargeProvider, int maxItemsPerBag)
        {
            _pricingList = pricingList;
            _bagChargeProvider = bagChargeProvider;
            _maxItemsPerBag = maxItemsPerBag;
            _basket = new List<string>();
        }

        public void Scan(string item)
        {
            _basket.Add(item);
        }

        public int ItemsInBasket
        {
            get { return _basket.Count; }
        }

        public int GetTotalPrice()
        {
            var total = _basket.Sum(item => _pricingList.Items.First(x => x.SKU == item).UnitPrice);
            var totalAfterDiscounts = total - ApplyDiscounts();
            return totalAfterDiscounts + _bagChargeProvider.GetBagCharge(_basket.Count, _maxItemsPerBag);
        }

        private int ApplyDiscounts()
        {
            var discountTotal = 0;
            discountTotal += CalculateDiscountTotal("A");
            discountTotal += CalculateDiscountTotal("B");
            return discountTotal;
        }

        private int CalculateDiscountTotal(string item)
        {
            var discountTotal = 0;
            var totalNumberofItems = _basket.FindAll(e => e == item).Count;

            if (totalNumberofItems > 0)
            {
                var selectedItem = _pricingList.Items.First(x => x.SKU == item);
                if (totalNumberofItems >= selectedItem.SpecialPricing.Quantity)
                {
                    var originalPrice = selectedItem.UnitPrice * selectedItem.SpecialPricing.Quantity;
                    discountTotal += originalPrice - selectedItem.SpecialPricing.Price;
                    discountTotal *= totalNumberofItems/selectedItem.SpecialPricing.Quantity;
                }
            }
            return discountTotal;
        }
    }
}