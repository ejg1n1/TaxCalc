using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class AddressStatus : BaseEntity
{
    [Required] public string Description { get; set; } = String.Empty;
}