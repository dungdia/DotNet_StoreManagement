using AutoMapper;
using DotNet_StoreManagement.Domain.entities;
using System.ComponentModel.DataAnnotations;

namespace DotNet_StoreManagement.Features.CategoryAPI.dtos
{
    [AutoMap(typeof(Category), ReverseMap = true)]
    public class CategoryDTO
    {
        [MinLength(3, ErrorMessage = "Tên loại phải từ 3 ký tự trở lên")]
        public string Name { get; set; } = null!;
    }
}
