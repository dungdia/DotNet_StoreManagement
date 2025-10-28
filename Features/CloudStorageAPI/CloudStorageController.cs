using System.Net;
using CloudinaryDotNet.Actions;
using DotNet_StoreManagement.Domain.entities.@base;
using DotNet_StoreManagement.SharedKernel.utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_StoreManagement.Features.CloudStorageAPI;

[ApiController]
[Route("api/v1/image/")]
public class CloudStorageController : ControllerBase
{
    private readonly CloudStorageService _service;

    public CloudStorageController(
        CloudinaryConfig config,
        CloudStorageService service
    )
    {
        _service = service;
    }

    [HttpGet("check-connection")]
    public async Task<IActionResult> checkConnection()
    {
        var isConnected = await _service.isConnectedAsync();
        if (!isConnected) throw new Exception("Can't connection");

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Connect cloudinary successfully"
        );

        return StatusCode(response.statusCode, response);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> uploadImageAPI(
        UploadRequest req
    )
    {
        var result = (ImageUploadResult) await _service.uploadImage(req);

        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Upload image successfully",
            result.Url
        );

        return StatusCode(response.statusCode, response);
    }
}