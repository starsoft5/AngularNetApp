using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class InvoiceOrder
{
    public int Id { get; set; }

    public int? InvoiceId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal? LineTotal { get; set; }

    public virtual InvoiceDetail? Invoice { get; set; }

    public virtual Product? Product { get; set; }
}
