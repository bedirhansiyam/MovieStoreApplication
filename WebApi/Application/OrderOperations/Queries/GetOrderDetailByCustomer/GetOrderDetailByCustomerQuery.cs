using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetailByCustomer;

public class GetOrderDetailByCustomerQuery
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int OrderedCustomerId { get; set; }
    public GetOrderDetailByCustomerQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<OrderDetailByCustomerViewModel> Handle()
    {
        var order = _dbContext.Orders.Include(x => x.Movie).Where(x => x.CustomerId == OrderedCustomerId).ToList();
        if(order is null)
            throw new InvalidOperationException("This customer doesn't have any order.");

        List<OrderDetailByCustomerViewModel> vm = _mapper.Map<List<OrderDetailByCustomerViewModel>>(order);

        return vm;
    }

    public class OrderDetailByCustomerViewModel
    {
        public string MovieName { get; set; }
        public string PurchaseDate { get; set; }
        public decimal Price { get; set; }

    }
}