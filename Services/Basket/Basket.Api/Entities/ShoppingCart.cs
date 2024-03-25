namespace Basket.Api.Entities;

public class ShoppingCart
{
    public ShoppingCart()
    {
        
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; } = null!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice
    {
        get
        {
            return Items.Select(x => x.Price * x.Quantity).Sum();
        }
    }
}