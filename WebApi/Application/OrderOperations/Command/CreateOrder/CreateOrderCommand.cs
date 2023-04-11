using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateOrderModel Model;
    public CreateOrderCommand(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == Model.MovieId);
        if(movie is null)
            throw new InvalidOperationException("Movie not found!");

        var order = _mapper.Map<Order>(Model);
        order.PurchaseDate = DateTime.Now;
        order.Price = movie.Price;
        order.CustomerId = LoginCustomer.loginCustomer.Id;

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

    public class CreateOrderModel
    {
        public int MovieId { get; set; }
        public decimal Price { get; set; }
    }
}