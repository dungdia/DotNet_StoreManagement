using DotNet_StoreManagement.SharedKernel.configuration;
using DotNet_StoreManagement.SharedKernel.exception;

namespace DotNet_StoreManagement.Features.CloudStorageAPI;

[Service]
public class CloudStorageService
{
    public ICloudStorge createInstance()
    {
        return new CloudinaryStorage(new CloudinaryConfig());
    }
    
    public async Task<Boolean> isConnectedAsync()
    {
        var instance = createInstance();
        return await instance.isConnectedAsync();
    }

    public async Task<Object> uploadImage(UploadRequest req)
    {
        if (req.file.Count == 0) throw new Exception("No files uploaded.");
        if(!req.file[0].ContentType.Contains("image")) throw APIException.BadRequest("File is not image");
        
        var instance = createInstance();
        
        return await instance.uploadImageAsync(req.file[0]);
    }
}