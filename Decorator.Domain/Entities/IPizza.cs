using System;

namespace Decorator.Domain.Entities
{
    public interface IPizza
    {
        Guid? Id { get; set; }
        int Size { get; set; }
        Quantity Cheese { get; set; }
        Quantity Tomato { get; set; }
        decimal Cost { get; }
        Order Order { get; set; }
        IPizza Self { get; }
    }
}
