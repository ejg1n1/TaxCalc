using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Domain.Entities.TaxCalcModels;

namespace Application.Services;

public class ReceiptService : IReceiptService
{
    public ReceiptResponse GenerateReceipt(List<ItemRequest> items)
    {
        var receipt = new Receipt();
        
        foreach (var item in items)
        {
            receipt.AddItem(new Item(item.Name, item.Price, item.IsImported, item.IsExempt));
        }
        
        receipt.CalculateTotals();
        
        var outputDto = new ReceiptResponse()
        {
            Items = receipt.Items.Select(i => $"{i.Name}: {i.Price + receipt.CalculateTax(i):0.00}").ToList(),
            SalesTaxes = receipt.SalesTaxes,
            Total = receipt.Total
        };
        
        return outputDto;
    }
}