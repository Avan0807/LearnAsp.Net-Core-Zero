using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Web.Services;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Payment;

public class CallbackModel : TestProjectPageModel
{
    private readonly VnPayService _vnPayService;
    private readonly IRepository<Entities.Payment, Guid> _paymentRepository;

    public CallbackModel(
        VnPayService vnPayService,
        IRepository<Entities.Payment, Guid> paymentRepository)
    {
        _vnPayService = vnPayService;
        _paymentRepository = paymentRepository;
    }

    public bool Success { get; set; }
    public string OrderId { get; set; } = "";
    public string TransactionId { get; set; } = "";
    public decimal Amount { get; set; }
    public string BankCode { get; set; } = "";
    public string CardType { get; set; } = "";
    public DateTime PaymentDate { get; set; }
    public string Message { get; set; } = "";

    public async Task OnGetAsync()
    {
        // Xử lý callback từ VNPay
        var response = _vnPayService.ProcessCallback(Request.Query);

        Success = response.Success;
        OrderId = response.OrderId ?? "";
        TransactionId = response.TransactionId ?? "";
        Amount = response.Amount;
        BankCode = response.BankCode ?? "N/A";
        CardType = response.CardType ?? "N/A";
        PaymentDate = response.PaymentDate;
        Message = response.Message ?? "Unknown error";

        // Cập nhật payment trong database nếu có OrderId
        if (!string.IsNullOrEmpty(OrderId))
        {
            try
            {
                var payments = await _paymentRepository.GetListAsync();
                var payment = payments.FirstOrDefault(p => p.OrderId == OrderId);

                if (payment != null)
                {
                    payment.Status = Success ? "Success" : "Failed";
                    payment.TransactionNo = TransactionId;
                    payment.BankCode = BankCode;
                    payment.CardType = CardType;
                    payment.PaymentDate = PaymentDate;

                    await _paymentRepository.UpdateAsync(payment);
                }
            }
            catch
            {
                // Ignore database errors
            }
        }
    }
}