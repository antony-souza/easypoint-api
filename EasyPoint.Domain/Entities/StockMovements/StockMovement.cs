using EasyPoint.Domain.Common.Entities;

namespace EasyPoint.Domain.Entities.StockMovement;

public class StockMovement : Entity
{
    public Guid StockId { get; set; }
    public Stock Stock { get; set; } = null!;
    public bool IsExit { get; set; }
    public int Quantity { get; set; }
}