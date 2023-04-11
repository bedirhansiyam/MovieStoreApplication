using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetail;


public class GetOrderDetailQuery
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int OrderId;

    public GetOrderDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public OrderDetailViewModel Handle()
    {
        var order = _dbContext.Orders.Include(x => x.Customer).Include(x => x.Movie).Where(x => x.Id == OrderId).SingleOrDefault();
        if(order is null)
            throw new InvalidOperationException("The order doesn't exist.");

        OrderDetailViewModel vm = _mapper.Map<OrderDetailViewModel>(order);

        return vm;
    }

    public class OrderDetailViewModel
    {
        public string MovieName { get; set; }
        public string CustomerName { get; set; }
        public string PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}