namespace DotNet_StoreManagement.Features.CloudStorageAPI;

public interface ICloudStorge
{
    Task<Boolean> isConnectedAsync();
    Task<Object> uploadImageAsync(IFormFile file);
}