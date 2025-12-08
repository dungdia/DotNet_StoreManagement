using System.Net;

namespace DotNet_StoreManagement.SharedKernel.utils;

public static class HttpStatusCodeExtensions
{
    public static int value(this HttpStatusCode statusCode)
    {
        return (int)statusCode;
    }
}