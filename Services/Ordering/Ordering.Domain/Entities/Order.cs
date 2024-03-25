using Ordering.Domain.Common;

namespace Ordering.Domain.Entities;

public class Order : BaseEntity
{
    public string UserName { get; set; }    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}