namespace Ordering.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}