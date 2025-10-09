using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TestProject.Web.Services;

public class VnPayService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string _tmnCode = "BK7RPQ9S";
    private readonly string _hashSecret = "NC040KH1SQRUSCNLV18ILCQULG9HOUNG";
    private readonly string _vnpUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";

    public VnPayService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string CreatePaymentUrl(string orderId, decimal amount, string orderInfo, string returnUrl)
    {
        var vnpay = new VnPayLibrary();

        vnpay.AddRequestData("vnp_Version", "2.1.0");
        vnpay.AddRequestData("vnp_Command", "pay");
        vnpay.AddRequestData("vnp_TmnCode", _tmnCode);
        vnpay.AddRequestData("vnp_Amount", ((long)(amount * 100)).ToString());
        vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
        vnpay.AddRequestData("vnp_CurrCode", "VND");
        vnpay.AddRequestData("vnp_IpAddr", GetIpAddress());
        vnpay.AddRequestData("vnp_Locale", "vn");
        vnpay.AddRequestData("vnp_OrderInfo", orderInfo);
        vnpay.AddRequestData("vnp_OrderType", "other");
        vnpay.AddRequestData("vnp_ReturnUrl", returnUrl);
        vnpay.AddRequestData("vnp_TxnRef", orderId);

        string paymentUrl = vnpay.CreateRequestUrl(_vnpUrl, _hashSecret);
        return paymentUrl;
    }

    public VnPayResponse ProcessCallback(IQueryCollection queries)
    {
        try
        {
            if (queries == null || !queries.Any())
            {
                return new VnPayResponse
                {
                    Success = false,
                    OrderId = "",
                    TransactionId = "",
                    Amount = 0,
                    BankCode = "",
                    CardType = "",
                    PaymentDate = DateTime.Now,
                    Message = "No payment data received. Transaction may have been cancelled."
                };
            }

            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in queries)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value);
                }
            }

            string orderId = vnpay.GetResponseData("vnp_TxnRef") ?? "";
            string transactionNo = vnpay.GetResponseData("vnp_TransactionNo") ?? "";
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode") ?? "";
            string vnp_SecureHash = queries["vnp_SecureHash"].ToString() ?? "";

            if (string.IsNullOrEmpty(orderId))
            {
                return new VnPayResponse
                {
                    Success = false,
                    OrderId = "",
                    TransactionId = "",
                    Amount = 0,
                    BankCode = "",
                    CardType = "",
                    PaymentDate = DateTime.Now,
                    Message = "Payment cancelled or incomplete data"
                };
            }

            bool checkSignature = false;
            if (!string.IsNullOrEmpty(vnp_SecureHash))
            {
                checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _hashSecret);
            }

            if (!checkSignature)
            {
                return new VnPayResponse
                {
                    Success = false,
                    OrderId = orderId,
                    TransactionId = "",
                    Amount = 0,
                    BankCode = "",
                    CardType = "",
                    PaymentDate = DateTime.Now,
                    Message = "Invalid signature"
                };
            }

            long vnpayTranId = 0;
            if (!string.IsNullOrEmpty(transactionNo))
            {
                long.TryParse(transactionNo, out vnpayTranId);
            }

            DateTime paymentDate = DateTime.Now;
            string payDateStr = vnpay.GetResponseData("vnp_PayDate") ?? "";
            if (!string.IsNullOrEmpty(payDateStr) && payDateStr.Length == 14)
            {
                DateTime.TryParseExact(payDateStr, "yyyyMMddHHmmss", null,
                    System.Globalization.DateTimeStyles.None, out paymentDate);
            }

            decimal amount = 0;
            string amountStr = vnpay.GetResponseData("vnp_Amount") ?? "";
            if (!string.IsNullOrEmpty(amountStr))
            {
                if (decimal.TryParse(amountStr, out decimal parsedAmount))
                {
                    amount = parsedAmount / 100;
                }
            }

            return new VnPayResponse
            {
                Success = vnp_ResponseCode == "00",
                OrderId = orderId,
                TransactionId = vnpayTranId > 0 ? vnpayTranId.ToString() : transactionNo,
                Amount = amount,
                BankCode = vnpay.GetResponseData("vnp_BankCode") ?? "N/A",
                CardType = vnpay.GetResponseData("vnp_CardType") ?? "N/A",
                PaymentDate = paymentDate,
                Message = GetResponseMessage(vnp_ResponseCode)
            };
        }
        catch (Exception ex)
        {
            return new VnPayResponse
            {
                Success = false,
                OrderId = "",
                TransactionId = "",
                Amount = 0,
                BankCode = "",
                CardType = "",
                PaymentDate = DateTime.Now,
                Message = "Error processing callback: " + ex.Message
            };
        }
    }

    private string GetResponseMessage(string responseCode)
    {
        return responseCode switch
        {
            "00" => "Payment successful",
            "07" => "Transaction suspected of fraud",
            "09" => "Card not registered for Internet Banking",
            "10" => "Incorrect OTP verification",
            "11" => "Payment timeout",
            "12" => "Card locked",
            "13" => "Invalid OTP",
            "24" => "Transaction cancelled by user",
            "51" => "Insufficient balance",
            "65" => "Transaction limit exceeded",
            "75" => "Payment gateway under maintenance",
            _ => $"Payment failed (Code: {responseCode})"
        };
    }

    private string GetIpAddress()
    {
        var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        return ipAddress ?? "127.0.0.1";
    }
}

public class VnPayResponse
{
    public bool Success { get; set; }
    public string OrderId { get; set; } = "";
    public string TransactionId { get; set; } = "";
    public decimal Amount { get; set; }
    public string BankCode { get; set; } = "";
    public string CardType { get; set; } = "";
    public DateTime PaymentDate { get; set; }
    public string Message { get; set; } = "";
}