namespace WebApplication1.Models
{
    public class ProductClient
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public virtual ICollection<ProductClientDetail> InvoiceOrders { get; set; } = new List<ProductClientDetail>();
    }
}
