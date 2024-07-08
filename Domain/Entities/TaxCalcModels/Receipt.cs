namespace Domain.Entities.TaxCalcModels;

public class Receipt
{
    public List<Item> Items { get; set; }
    public decimal SalesTaxes { get; set; }
    public decimal Total { get; set; }

    public Receipt()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void CalculateTotals()
    {
        SalesTaxes = Items.Sum(i => CalculateTax(i));
        Total = Items.Sum(i => i.Price) + SalesTaxes;
    }

    public decimal CalculateTax(Item item)
    {
        decimal tax = 0;
        if (!item.IsExempt)
        {
            tax += RoundUpToNearest0_05(item.Price * 0.10m);
        }
        if (item.IsImported)
        {
            tax += RoundUpToNearest0_05(item.Price * 0.05m);
        }
        return tax;
    }

    public decimal RoundUpToNearest0_05(decimal amount)
    {
        return Math.Ceiling(amount * 20) / 20;
    }

    public override string ToString()
    {
        var receipt = new List<string>();
        foreach (var item in Items)
        {
            receipt.Add($"{item.Name}: {item.Price + CalculateTax(item):0.00}");
        }
        receipt.Add($"Sales Taxes: {SalesTaxes:0.00}");
        receipt.Add($"Total: {Total:0.00}");
        return string.Join(Environment.NewLine, receipt);
    }
}