namespace DotNet_StoreManagement.SharedKernel.utils.impl;

public interface IPasswordHasher
{
    String hashPassword(String username, String password);
    Boolean verifyPassword(String username,String hashedPassword, String providedPassword);
}