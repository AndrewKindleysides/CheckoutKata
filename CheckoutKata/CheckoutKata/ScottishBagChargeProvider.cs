namespace CheckoutKata
{
    public class ScottishBagChargeProvider : IBagChargeProvider
    {
        private const int BagCharge = 5;

        public int GetBagCharge(int itemsInBasket, int maxItemsPerBag)
        {
            if (itemsInBasket <= maxItemsPerBag)
                return BagCharge;

            return (itemsInBasket / maxItemsPerBag + 1) * BagCharge;
        }
    }
}