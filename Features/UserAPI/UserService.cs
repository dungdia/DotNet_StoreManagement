using AutoMapper;
using CloudinaryDotNet.Actions;
using DotNet_StoreManagement.Domain.entities;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.Domain.enums;
using DotNet_StoreManagement.Features.UserAPI.dtos;
using DotNet_StoreManagement.Features.UserAPI.impl;
using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;
using DotNet_StoreManagement.SharedKernel.persistence;
using DotNet_StoreManagement.SharedKernel.utils;

namespace DotNet_StoreManagement.Features.UserAPI
{
    [Service]
    public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly PBKDF2PasswordHasher _passwordHasher;

        public UserService(IUserRepository repo, IMapper mapper, PBKDF2PasswordHasher passwordHasher)
        {
            _repo = repo;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Page<UserResponseDTO>> GetPageableUserAsync(UserFilterDTO? dtoFilter, PageRequest pageRequest)
        {
            IQueryable<User> query = _repo.GetQueryable();

            query = query
                .Filter("Username", dtoFilter?.Username, FilterType.CONTAINS)
                .Filter("FullName", dtoFilter?.FullName, FilterType.CONTAINS)
                .Filter("Role", dtoFilter?.Role, FilterType.CONTAINS);
            var userPage = await _repo.FindAllPageAsync(
                query,
                pageRequest.PageNumber,
                pageRequest.PageSize
            );
            var userDtoContent = _mapper.Map<List<UserResponseDTO>>(userPage.Content);
            var userDtoPage = new Page<UserResponseDTO>
            {
                Content = userDtoContent,
                PageNumber = userPage.PageNumber,
                PageSize = userPage.PageSize,
                TotalElements = userPage.TotalElements,
                TotalPages = userPage.TotalPages
            };
            return userDtoPage;
        }

        public async Task<User> CreateUserAsync(UserDTO dto)
        {
            var existingUser = await _repo.GetByUsernameAsync(dto.Username);

            if (existingUser != null)
            {
                throw APIException.BadRequest($"Username '{dto.Username}' is already taken.");
            }

            var user = _mapper.Map<User>(dto);
            user.CreatedAt = DateTime.Now;

            user.Password = _passwordHasher.hashPassword(user.Username, user.Password);

            await _repo.AddAsync(user);
            var affectedRows = await _repo.SaveChangesAsync();

            if (affectedRows == 0)
                throw APIException.InternalServerError("Create user failed");

            return user;
        }

        public async Task<UserResponseDTO?> EditUser(int id, UpdaterUserDTO dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw APIException.BadRequest("Invalid User's ID");

            // Chỉ cập nhật khi userName thật sự thay đổi
            if (user.Username != dto.Username)
            {
                var existingUser = await _repo.GetByUsernameAsync(dto.Username);

                if (existingUser != null)
                {
                    throw APIException.BadRequest($"Username '{dto.Username}' is already taken.");
                }
            }

            _mapper.Map(dto, user);



            var affectedRows = await _repo.SaveChangesAsync();

            if (affectedRows == 0)
                throw APIException.InternalServerError("Update user failed");

            var resultDTO = _mapper.Map<UserResponseDTO>(user);

            return resultDTO;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw APIException.BadRequest("Invalid User's ID");
            await _repo.DeleteAsync(user);
            var affectedRows = await _repo.SaveChangesAsync();
            if (affectedRows == 0)
                throw APIException.InternalServerError("Delete user failed");
        }

    }
}
