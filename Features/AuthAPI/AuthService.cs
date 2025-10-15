using AutoMapper;
using CloudinaryDotNet;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Features.AuthAPI.repositories;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.security;
using DotNet_StoreManagement.SharedKernel.utils;

namespace DotNet_StoreManagement.Features.AuthAPI;

[Service]
public class AuthService
{
    private readonly UserRepository        _userRepo;
    private readonly JwtTokenProvider      _tokenProvider;
    private readonly PBKDF2PasswordHasher  _passwordHasher;
    private readonly IMapper               _mapper;
    
    public AuthService(
        UserRepository       userRepo,
        JwtTokenProvider     tokenProvider,
        PBKDF2PasswordHasher passwordHasher,
        IMapper              mapper
    )
    {
        _userRepo       = userRepo;
        _tokenProvider  = tokenProvider;
        _passwordHasher = passwordHasher;
        _mapper         = mapper;
    }
    
    public async Task<User> register(UserDTO dto)
    {
        var user = _mapper.Map<User>(dto);
        user.Password = _passwordHasher.hashPassword(user.Username, user.Password);
        user.Role = "staff";
        user.CreatedAt = DateTime.Now;
        
        var affectedRows = await _userRepo.AddAndSaveAsync(user);
        if(affectedRows < 0) throw new ApplicationException("Failed to register user");
        
        return user;
    }
    
    public async Task<Object> authenticate(UserDTO req)
    {
        var user = await _userRepo.FindUserByUsername(req.Username);

        if (user == null) throw APIException.BadRequest("Can't find username");

        Boolean checkPassword = _passwordHasher.verifyPassword(user.Username, req.Password, user.Password);
        
        if(!checkPassword) throw APIException.BadRequest("Wrong password");
        
        String accessToken = await _tokenProvider.generateAccessToken(user);

        return new 
        {
            access_token = accessToken
        };
    }
}