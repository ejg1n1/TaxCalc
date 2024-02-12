using Athena.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class PostStatus : BaseEntity
{
    [Required]
    public string Description { get; set; } = String.Empty;
}