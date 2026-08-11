using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Stocks;
using EasyPoint.Domain.Enums.StockMovementsTypes;

namespace EasyPoint.Domain.Entities.StockMovements;

public class StockMovement : Entity
{
    public Guid StockId { get; set; }
    public Stock Stock { get; set; } = null!;
    public StockMovementType Type { get; set; }
    public int Quantity { get; set; }
    public int BeforeQuantity { get; set; }
    public int AfterQuantity { get; set; }
}
