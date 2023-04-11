using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations;

public class TokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateAccessToken(Customer customer)
    {
        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        tokenModel.ExpirationDate = DateTime.Now.AddMinutes(20);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer:_configuration["Token:Issuer"],
            audience:_configuration["Token:Audience"],
            expires: tokenModel.ExpirationDate,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}