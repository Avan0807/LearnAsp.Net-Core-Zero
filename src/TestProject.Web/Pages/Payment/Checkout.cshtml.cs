using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using TestProject.Web.Services;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Payment;

public class CheckoutModel : TestProjectPageModel
{
    private readonly VnPayService _vnPayService;
    private readonly IRepository<Entities.Payment, Guid> _paymentRepository;

    public CheckoutModel(
        VnPayService vnPayService,
        IRepository<Entities.Payment, Guid> paymentRepository)
    {
        _vnPayService = vnPayService;
        _paymentRepository = paymentRepository;
    }

    [BindProperty]
    public string OrderInfo { get; set; }

    [BindProperty]
    public decimal Amount { get; set; }

    public void OnGet()
    {
        OrderInfo = "Test payment order";
        Amount = 100000;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Tạo mã đơn hàng unique
        string orderId = DateTime.Now.Ticks.ToString();

        // Lưu payment vào database
        var payment = new Entities.Payment(
            Guid.NewGuid(),
            orderId,
            Amount,
            OrderInfo
        );
        await _paymentRepository.InsertAsync(payment);

        // Tạo URL return - Tự động lấy từ request (ngrok hoặc localhost)
        string baseUrl = $"{Request.Scheme}://{Request.Host}";
        string returnUrl = $"{baseUrl}/Payment/Callback";

        // Tạo URL thanh toán VNPay
        string paymentUrl = _vnPayService.CreatePaymentUrl(orderId, Amount, OrderInfo, returnUrl);

        // Redirect sang VNPay
        return Redirect(paymentUrl);
    }
}