namespace WebApplication1.Models
{
    public class ProductClientDetail
    {
        public int Id { get; set; }

        public int? InvoiceId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal? LineTotal { get; set; }

        public virtual InvoiceDetail? Invoice { get; set; }

        public virtual Product? Product { get; set; }

        // Assuming this is the missing collection
        public virtual ICollection<InvoiceOrder> InvoiceOrders { get; set; } = new List<InvoiceOrder>();

    }
}
