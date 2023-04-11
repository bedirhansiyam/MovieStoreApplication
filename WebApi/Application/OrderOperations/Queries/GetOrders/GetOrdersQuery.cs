using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrders;

public class GetOrderQuery
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<OrderViewModel> Handle()
    {
        var orderList = _dbContext.Orders.Include(x => x.Customer).Include(x => x.Movie).OrderBy(x => x.Id).ToList();

        List<OrderViewModel> vm = _mapper.Map<List<OrderViewModel>>(orderList);

        return vm;
    }

    public class OrderViewModel
    {
        public string MovieName { get; set; }
        public string CustomerName { get; set; }
        public string PurchaseDate { get; set; }
        public decimal Price { get; set; }

    }
}