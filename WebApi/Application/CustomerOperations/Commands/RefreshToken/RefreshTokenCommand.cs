using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.CustomerOperations.Commands.RefreshToken;

public class RefreshTokenCommand
{
    public string RefreshToken { get; set; }
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var customer = _dbContext.Customers.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
        if(customer is not null)
        {
            TokenHandler tokenHandler = new TokenHandler(_configuration);
            Token token = tokenHandler.CreateAccessToken(customer);

            customer.RefreshToken = token.RefreshToken;
            customer.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);
            _dbContext.SaveChanges();
            return token; 
        }
        else
            throw new InvalidOperationException("No valid refresh token found.");
    }
}