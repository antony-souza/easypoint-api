using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.Stocks;

public class Stock : Entity
{
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
}