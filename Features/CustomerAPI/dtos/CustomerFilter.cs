namespace DotNet_StoreManagement.Features.CustomerAPI.dtos
{
    public class CustomerFilter
    {
        public int? CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
