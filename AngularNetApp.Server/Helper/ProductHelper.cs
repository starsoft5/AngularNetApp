using WebApplication1.Models;

namespace WebApplication1.Helper
{
    public class ProductHelper
    {
        public static void CreateInvoiceWithOrders(ProductClient productClient,HeavensPlaceContext heavensPlaceContext)
        {
            using (var context = heavensPlaceContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    InvoiceDetail invoiceDetail = new InvoiceDetail();

                    invoiceDetail.CustomerName = productClient.CustomerName.Trim();
                    invoiceDetail.InvoiceNumber = productClient.InvoiceNo.Trim();
                    invoiceDetail.InvoiceDate = productClient.InvoiceDate;
                    context.InvoiceDetails.Add(invoiceDetail);
                    context.SaveChanges(); // Save changes

                    // Get the generated primary key
                    //int invoiceDetailsId = invoiceDetail.Id;

                    foreach (ProductClientDetail productClientDetail in productClient.InvoiceOrders)
                    {
                        invoiceDetail.InvoiceOrders.Add(new InvoiceOrder
                        {
                            InvoiceId = invoiceDetail.Id,
                            ProductId = productClientDetail.ProductId,
                            Quantity = productClientDetail.Quantity,
                            Price = productClientDetail.Price,
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    var message = ex.ToString();
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public static bool IsInvoiceNoDuplicate(string invoiceNo,  HeavensPlaceContext heavensPlaceContext)
        {
            var invoiceNoExists = heavensPlaceContext.InvoiceDetails
                .Any(p => p.InvoiceNumber.Trim().ToLower() == invoiceNo.Trim().ToLower());

            if (invoiceNoExists)
                return true;

            return false;

        }

    }
}
