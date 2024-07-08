namespace Application.Models.REST.Response;

public class ReceiptResponse
{
    public List<string> Items { get; set; }
    public decimal SalesTaxes { get; set; }
    public decimal Total { get; set; }
}