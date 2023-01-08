namespace Basket.Api.Entity
{
    public class ShoppingCart
    {
        public string? UserName { get; set; }
        public ICollection<ShoppingCartItem>? Items { get; set; }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                Items?.ToList().ForEach(x => totalPrice += x.Price * x.Quantity);
                return totalPrice;
            }
        }
    }
}
