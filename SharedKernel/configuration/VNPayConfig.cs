using VNPAY.Extensions;

namespace DotNet_StoreManagement.SharedKernel.configuration;

public static class VNPayConfigs
{
     public static IServiceCollection VNConfig(
          this IServiceCollection services,
          IConfiguration config
     )
     {
          services.AddVnpayClient(c => {
               var vnpayConfig = config.GetSection("VNPAY");
               c.TmnCode = vnpayConfig["TmnCode"]!;
               c.HashSecret = vnpayConfig["HashSecret"]!;
               c.CallbackUrl = vnpayConfig["CallbackUrl"]!;
          });
          return services;
     }
}