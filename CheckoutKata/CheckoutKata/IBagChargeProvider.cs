namespace CheckoutKata
{
    public interface IBagChargeProvider
    {
        int GetBagCharge(int itemsInBasket, int maxItemsPerBag);
    }
}