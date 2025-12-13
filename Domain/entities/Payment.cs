using System;
using System.Collections.Generic;

namespace DotNet_StoreManagement.Domain.entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Status { get; set; }

    public long? TransactionRef { get; set; }
}
