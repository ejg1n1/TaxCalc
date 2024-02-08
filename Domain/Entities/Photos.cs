namespace Core.Entities;

public class Photos : BaseEntity
{
    public virtual Post Post { get; set; } = null!;
}