using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Orders
{
    public static void AddOrders(this MovieStoreDbContext context)
    {
        context.Orders.AddRange(
            new Order
            {
                MovieId = 1,
                CustomerId = 2,
                
            },
            new Order
            {
                MovieId = 3,
                CustomerId = 1,
                
            }
        );
    }
}