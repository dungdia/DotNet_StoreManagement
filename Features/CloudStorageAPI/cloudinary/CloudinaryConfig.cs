using DotNet_StoreManagement.SharedKernel.configuration;

namespace DotNet_StoreManagement.Features.CloudStorageAPI;

[Configuration]
public class CloudinaryConfig
{
    public CloudinaryDotNet.Cloudinary cloudinary()
    {
        var _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();
        
        var config = _config["Cloudinary:url"];
        Console.WriteLine(config);
        CloudinaryDotNet.Cloudinary cloud = new CloudinaryDotNet.Cloudinary(config);
        cloud.Api.Secure = true;
        return cloud;
    }
}