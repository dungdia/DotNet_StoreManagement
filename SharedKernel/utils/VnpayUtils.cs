namespace DotNet_StoreManagement.SharedKernel.utils;

public class VnpayUtils
{
    public static string GetVnpayErrorMessage(string responseCode)
    {
        return responseCode switch
        {
            "00" => "Giao dịch thành công",
            "07" => "Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường).",
            "09" => "Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng.",
            "10" => "Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần.",
            "11" => "Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch.",
            "24" => "Khách hàng hủy giao dịch.",
            "51" => "Tài khoản của quý khách không đủ số dư để thực hiện giao dịch.",
            "65" => "Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày.",
            "75" => "Ngân hàng thanh toán đang bảo trì.",
            "79" => "Nhập sai mật khẩu thanh toán quá số lần quy định. Xin quý khách vui lòng thực hiện lại giao dịch.",
            "99" => "Lỗi không xác định.",
            _ => "Lỗi giao dịch không xác định."
        };
    }
}
