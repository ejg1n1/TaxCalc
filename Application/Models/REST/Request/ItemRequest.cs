namespace Application.Models.REST.Request;

public class ItemRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsImported { get; set; }
    public bool IsExempt { get; set; }
}