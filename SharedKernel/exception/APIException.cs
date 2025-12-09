namespace DotNet_StoreManagement.SharedKernel.exception;

public class APIException : Exception
{
    public int statusCode { get; set; }
    public String message { get; set; }


    public APIException()
    {
    }

    public APIException(int statusCode, String message) : base(message)
    {
        this.statusCode = statusCode;
        this.message = message;
    }
    
    public static APIException BadRequest(string message) =>
        new APIException(StatusCodes.Status400BadRequest, message);

    public static APIException Forbidden(string message) =>
        new APIException(StatusCodes.Status403Forbidden, message);
    
    public static APIException InternalServerError(string message) =>
        new APIException(StatusCodes.Status500InternalServerError, message);
}