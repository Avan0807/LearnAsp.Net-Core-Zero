using System;
using Volo.Abp.Domain.Entities;

namespace TestProject.Entities;

public class Payment : Entity<Guid>
{
    public string OrderId { get; set; }
    public decimal Amount { get; set; }
    public string OrderInfo { get; set; }
    public string TransactionNo { get; set; }    // Nullable
    public string BankCode { get; set; }         // Nullable
    public string CardType { get; set; }         // Nullable
    public DateTime? PaymentDate { get; set; }   // Nullable - Thêm dấu ?
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }

    protected Payment() { }

    public Payment(Guid id, string orderId, decimal amount, string orderInfo) : base(id)
    {
        OrderId = orderId;
        Amount = amount;
        OrderInfo = orderInfo;
        Status = "Pending";
        CreatedDate = DateTime.Now;
    }
}