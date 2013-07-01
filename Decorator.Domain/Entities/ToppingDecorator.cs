using System;
using Decorator.Domain.Common;

namespace Decorator.Domain.Entities
{
    public class ToppingDecorator : Entity<Guid>, IPizza
    {
        public virtual IPizza BasePizza { get; set; }
        public virtual Order Order { get; set; }

        public ToppingDecorator()
        {
        }

        public ToppingDecorator(IPizza basePizza)
        {
            Id = Guid.NewGuid();
            BasePizza = basePizza;
        }

        public virtual int Size
        {
            get { return BasePizza.Size; }
            set { BasePizza.Size = value; }
        }

        public virtual Quantity Cheese
        {
            get { return BasePizza.Cheese; }
            set { BasePizza.Cheese = value; }
        }

        public virtual Quantity Tomato
        {
            get { return BasePizza.Tomato; }
            set { BasePizza.Tomato = value; }
        }

        public virtual decimal Cost
        {
            get { return BasePizza.Cost; }
        }

        public virtual new IPizza Self
        {
            get { return this; }
        }
    }
}
