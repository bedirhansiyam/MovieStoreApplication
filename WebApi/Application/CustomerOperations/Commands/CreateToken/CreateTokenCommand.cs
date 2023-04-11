using AutoMapper;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.CustomerOperations.Commands.CreateToken;

public class CreateTokenCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public CreateTokenModel Model;

    public CreateTokenCommand(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    public Token TokenHandle()
    {
        var customer = _dbContext.Customers.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
        if(customer is null)
            throw new InvalidOperationException("Email or password is incorrect!");

        TokenHandler tokenHandler = new TokenHandler(_configuration);
        Token token = tokenHandler.CreateAccessToken(customer);

        customer.RefreshToken = token.RefreshToken;
        customer.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);
        LoginCustomer.loginCustomer = customer;
        _dbContext.SaveChanges();

        return token;
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}