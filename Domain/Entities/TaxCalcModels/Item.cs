namespace Domain.Entities.TaxCalcModels;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsImported { get; set; }
    public bool IsExempt { get; set; }
    
    public Item(string name, decimal price, bool isImported, bool isExempt)
    {
        Name = name;
        Price = price;
        IsImported = isImported;
        IsExempt = isExempt;
    }
}