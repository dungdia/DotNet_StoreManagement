using System.Text.Json.Serialization;

namespace DotNet_StoreManagement.Domain.entities.@base;

public class APIResponse<T> : BaseResponse
{
    [JsonPropertyName("content")] 
    public T content { get; set; }
    [JsonPropertyName("metadata")]
    public Object? metadata { get; set; }
    
    public APIResponse() { }

    public APIResponse(int statusCode, string message) : base(statusCode, message)
    {
    }

    public APIResponse(
        int statusCode, 
        string message, 
        T data) : this(statusCode, message, data, null)
    {
    }

    public APIResponse(
        int statusCode, 
        String message, 
        T content, 
        Object? metadata
    ) : base(statusCode, message)
    {
        this.content = content;
        this.metadata = metadata;
    }

    public APIResponse<T> setMetadata(Object metadata)
    {
        return new APIResponse<T>(statusCode, message, content, metadata);
    }
}