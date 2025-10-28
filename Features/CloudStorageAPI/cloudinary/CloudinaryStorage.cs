using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DotNet_StoreManagement.SharedKernel.exception;

namespace DotNet_StoreManagement.Features.CloudStorageAPI;

public class CloudinaryStorage : ICloudStorge
{
    private readonly CloudinaryConfig _config;
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    
    public CloudinaryStorage(
        CloudinaryConfig config
    )
    {
        _config = config;
        _cloudinary = _config.cloudinary();
    }
    
    public async Task<Boolean> isConnectedAsync()
    {
        PingResult? ping = await _cloudinary.PingAsync();
        Boolean result = ping.StatusCode == HttpStatusCode.OK;
        return result;
    }

    public async Task<Object> uploadImageAsync(IFormFile file)
    {
        try
        {
            await using var stream = file.OpenReadStream();
            var publicId = Guid.NewGuid() + "." + file.ContentType.Split("/")[1];
        
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = publicId
            };
        
            Object result = await _cloudinary.UploadAsync(uploadParams);
            return result;
        }
        catch (Exception e)
        {
            throw APIException.InternalServerError(e.Message);
        }
    }
}