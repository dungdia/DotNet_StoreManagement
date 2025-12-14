using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.AuthAPI.repositories;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DotNet_StoreManagement.SharedKernel.security;

[Component]
public class JwtTokenProvider
{
    private readonly String _key;
    private readonly String _issuer;
    private readonly String _audience;
    private readonly String _atExpireTime; 
    private readonly String _rtExpireTime;

    private readonly UserRepository _userRepository;

    public JwtTokenProvider(
        IConfiguration config,
        UserRepository userRepository
    )
    {
        var atTime = new DataTable().Compute(config["jwt:atExpiryInMillisecond"], null).ToString();

        _key          = config["jwt:SecretKey"] ?? "";
        _issuer       = config["jwt:Issuer"]    ?? "";
        _audience     = config["jwt:Audience"]  ?? "";
        _atExpireTime = atTime ?? "";

        _userRepository = userRepository;
    }

    public async Task<String> generateAccessToken<TUser>(TUser user) where TUser : User
    {   
        var claims = new List<Claim>
        {
            new Claim("userId", user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", user.Role!),
        };
        
        string secretKey = _key;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var tokenDes = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMilliseconds(Convert.ToInt32(_atExpireTime)),
            signingCredentials: credentials
        );
        
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.WriteToken(tokenDes);
        
        return jwtToken;
    }

    public JwtSecurityToken? extractAllClaims(String token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken;
    }

    public JwtPayload? extractPayload(string token)
    {
        return extractAllClaims(token)!.Payload;
    }

    public Object? getValue<TUser>(TUser obj, string propertyName) where TUser : class
    {
        var prop = typeof(TUser).GetProperties()
            .FirstOrDefault(p => 
                string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase)
            );
        return prop?.GetValue(obj) ?? "";
    }

    public ClaimsPrincipal? validateToken(String token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        try
        {
            return tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
                ClockSkew = TimeSpan.Zero
            }, out _);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    
    // --- REFRESH TOKEN ---
    // public async Task<String> generateRefreshToken<TUser>(TUser user) where TUser : class
    // {
    //     var u = await _accountRepo.findByUsername(getValue(user, "username")?.ToString()!);
    //
    //     var dt = DateTime.UtcNow.AddMilliseconds(Convert.ToInt32(_rtExpireTime));
    //     
    //     RefreshToken rt = new RefreshToken();
    //     rt.Id = Guid.NewGuid().ToString();
    //     rt.Token = Guid.NewGuid().ToString();
    //     rt.AccountId = u?.Id;
    //     rt.ExpiryDate = TimeUtils.AsiaTimeZone(dt);
    //     
    //     // _refreshTokenRepository.add(rt);
    //     
    //     return rt.Token;
    // }
}