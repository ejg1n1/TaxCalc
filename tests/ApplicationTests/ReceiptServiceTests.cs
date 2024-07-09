using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace ApplicationTests;

public class ReceiptServiceTests
{
    private readonly IReceiptService _receiptService;

    public ReceiptServiceTests()
    {
        _receiptService = new ReceiptService();
    }

    [Fact]
    public void TestBasicTaxCalculation()
    {
        var items = new List<ItemRequest>
        {
            new ItemRequest { Name = "Music CD", Price = 14.99m, IsImported = false, IsExempt = false }
        };

        var receipt = _receiptService.GenerateReceipt(items);

        Assert.Equal(1.50m, receipt.SalesTaxes);
        Assert.Equal(16.49m, receipt.Total);
    }

    [Fact]
    public void TestImportDutyCalculation()
    {
        var items = new List<ItemRequest>
        {
            new ItemRequest { Name = "Imported bottle of perfume", Price = 47.50m, IsImported = true, IsExempt = false }
        };

        var receipt = _receiptService.GenerateReceipt(items);

        Assert.Equal(7.15m, receipt.SalesTaxes);
        Assert.Equal(54.65m, receipt.Total);
    }

    [Fact]
    public void TestExemptItems()
    {
        var items = new List<ItemRequest>
        {
            new ItemRequest { Name = "Book", Price = 12.49m, IsImported = false, IsExempt = true }
        };

        var receipt = _receiptService.GenerateReceipt(items);

        Assert.Equal(0.00m, receipt.SalesTaxes);
        Assert.Equal(12.49m, receipt.Total);
    }

    [Fact]
    public void TestCombinedTaxes()
    {
        var items = new List<ItemRequest>
        {
            new ItemRequest { Name = "Imported bottle of perfume", Price = 27.99m, IsImported = true, IsExempt = false }
        };

        var receipt = _receiptService.GenerateReceipt(items);

        Assert.Equal(4.20m, receipt.SalesTaxes);
        Assert.Equal(32.19m, receipt.Total);
    }

    [Fact]
    public void TestRoundingRule()
    {
        var items = new List<ItemRequest>
        {
            new ItemRequest { Name = "Imported bottle of perfume", Price = 47.50m, IsImported = true, IsExempt = false }
        };

        var receipt = _receiptService.GenerateReceipt(items);

        Assert.Equal(7.15m, receipt.SalesTaxes);
    }

    [Fact]
    public void TestReceiptGeneration()
    {
        var items = new List<ItemRequest>
        {
            new ItemRequest { Name = "Imported bottle of perfume", Price = 27.99m, IsImported = true, IsExempt = false },
            new ItemRequest { Name = "Bottle of perfume", Price = 18.99m, IsImported = false, IsExempt = false },
            new ItemRequest { Name = "Packet of paracetamol", Price = 9.75m, IsImported = false, IsExempt = true },
            new ItemRequest { Name = "Imported box of chocolates", Price = 11.25m, IsImported = true, IsExempt = true }
        };

        var receipt = _receiptService.GenerateReceipt(items);

        var expectedItems = new List<string>
        {
            "Imported bottle of perfume: 32.19",
            "Bottle of perfume: 20.89",
            "Packet of paracetamol: 9.75",
            "Imported box of chocolates: 11.85"
        };

        Assert.Equal(expectedItems, receipt.Items);
        Assert.Equal(6.70m, receipt.SalesTaxes);
        Assert.Equal(74.68m, receipt.Total);
    }
}
