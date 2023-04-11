using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Commands.DeleteOrder;

public class DeleteOrderCommand
{
    public int OrderId;
    private readonly IMovieStoreDbContext _dbContext;

    public DeleteOrderCommand(IMovieStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var order = _dbContext.Orders.SingleOrDefault(x => x.Id == OrderId);
        if(order is null)
            throw new InvalidOperationException("The order doesn't exist.");

        _dbContext.Orders.Remove(order);
        _dbContext.SaveChanges();
    }
}