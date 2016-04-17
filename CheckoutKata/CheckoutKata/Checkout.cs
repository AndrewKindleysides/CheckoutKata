using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
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

            var discountTotal = ApplyDiscounts();
            return total - discountTotal;
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
            if (_basket.FindAll(e => e == item).Count > 0)
            {
                var selectedItem = _pricingList.Items.First(x => x.SKU == item);
                if (_basket.FindAll(e => e == item).Count >= selectedItem.SpecialPricing.Quantity)
                {
                    var originalPrice = selectedItem.UnitPrice * selectedItem.SpecialPricing.Quantity;
                    discountTotal += originalPrice - selectedItem.SpecialPricing.Price;
                    discountTotal *= _basket.FindAll(e => e == item).Count/selectedItem.SpecialPricing.Quantity;
                }
            }
            return discountTotal;
        }
    }
}