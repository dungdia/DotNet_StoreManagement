namespace DotNet_StoreManagement.Features.CloudStorageAPI;

public class UploadRequest
{
    public List<IFormFile> file { get; set; }

    public UploadRequest()
    {
    }

    public UploadRequest(
        List<IFormFile> file
    )
    {
        this.file = file;
    }
}