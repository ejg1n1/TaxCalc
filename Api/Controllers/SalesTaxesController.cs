using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SalesTaxesController : BaseController
{
    private readonly IReceiptService _receiptService;

    public SalesTaxesController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpPost("generateReceipt")]
    public ActionResult<ReceiptResponse> GenerateReceipt([FromBody] List<ItemRequest> items)
    {
        if (items == null || items.Count == 0)
        {
            return BadRequest("No items provided.");
        }

        var receipt = _receiptService.GenerateReceipt(items);
        return Ok(receipt);
    }
}