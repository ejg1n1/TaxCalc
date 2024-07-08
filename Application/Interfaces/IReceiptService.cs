using Application.Models.REST.Request;
using Application.Models.REST.Response;

namespace Application.Interfaces;

public interface IReceiptService
{
    ReceiptResponse GenerateReceipt(List<ItemRequest> items);
}