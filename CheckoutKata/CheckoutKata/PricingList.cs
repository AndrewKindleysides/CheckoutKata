using System.Collections.Generic;

namespace CheckoutKata
{
    public class PricingList
    {
        public PricingList()
        {
            Items = new List<Item>
            {
                new Item() {SKU = "A", UnitPrice = 50, SpecialPricing = new SpecialPricing(){Quantity = 3, Price = 130}}, 
                new Item() {SKU = "B", UnitPrice = 30, SpecialPricing = new SpecialPricing(){Quantity = 2, Price = 45}},
                new Item() {SKU = "C", UnitPrice = 20},
                new Item() {SKU = "D", UnitPrice = 15}
            };
        }

        public List<Item> Items { get; set; }
    }
}