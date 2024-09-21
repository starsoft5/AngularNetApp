using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class InvoiceDetail
{
    public int Id { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public DateTime? InvoiceDate { get; set; }

    public decimal GrandTotal { get; set; }

    public virtual ICollection<InvoiceOrder> InvoiceOrders { get; set; } = new List<InvoiceOrder>();
}
