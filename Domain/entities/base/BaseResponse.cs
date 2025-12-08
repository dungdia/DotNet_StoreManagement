namespace DotNet_StoreManagement.Domain.entities.@base;

public class BaseResponse
{
    public int statusCode { get; set; }
    public String message { get; set; }

    public BaseResponse() { }

    public BaseResponse(int statusCode,String message)
    {
        this.statusCode = statusCode;
        this.message = message;
    }
}