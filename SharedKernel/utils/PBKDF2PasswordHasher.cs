using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.utils.impl;
using Microsoft.AspNetCore.Identity;

namespace DotNet_StoreManagement.SharedKernel.utils;

[Component]
public class PBKDF2PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<String> _passwordHasher = new();
    
    public string hashPassword(string username, string password)
    {
        return _passwordHasher.HashPassword(username, password);
    }

    public bool verifyPassword(string username, string providedPassword, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(username, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}