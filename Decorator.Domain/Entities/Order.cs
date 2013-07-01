using System;
using System.Collections.Generic;
using System.Linq;
using Decorator.Domain.Common;

namespace Decorator.Domain.Entities
{
    public class Order : Entity<Guid>
    {
        public virtual string CustomerName { get; set; }
        public virtual string DeliveryAddress { get; set; }
        public virtual IList<IPizza> Items { get; set; } 

        public static Order Create(string customerName, string deliveryAddress)
        {
            return new Order
                       {
                           Id = Guid.NewGuid(),
                           CustomerName = customerName,
                           DeliveryAddress = deliveryAddress,
                           Items = new List<IPizza>()
                       };
        }

        public virtual void Add(IPizza pizza)
        {
            Items.Add(pizza);
            pizza.Order = this;
        }

        public virtual decimal TotalCost
        {
            get
            {
                var pizzaTotal = Items.Sum(item => item.Cost);
                var deliveryCost = 5m;

                if(pizzaTotal < 20m)
                {
                    return pizzaTotal + deliveryCost;
                }
                else
                {
                    return pizzaTotal;
                }
            }
        }
    }
}
