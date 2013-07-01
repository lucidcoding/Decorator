using System;
using Decorator.Domain.Common;

namespace Decorator.Domain.Entities
{
    public class Pizza : Entity<Guid>, IPizza
    {
        public virtual int Size { get; set; }
        public virtual Quantity Cheese { get; set; }
        public virtual Quantity Tomato { get; set; }
        public virtual Order Order { get; set; }

        public static IPizza Create(int size, Quantity cheese, Quantity tomato)
        {
            return new Pizza
                       {
                           Id = Guid.NewGuid(),
                           Size = size,
                           Cheese = cheese,
                           Tomato = tomato
                       };
        }

        public virtual decimal Cost
        {
            get
            {
                decimal cost = 0;

                switch(Size)
                {
                    case 10:
                        cost += 6.99m;
                        break;
                    case 12:
                        cost += 7.99m;
                        break;
                    case 14:
                        cost += 8.99m;
                        break;
                }

                if(Cheese == Quantity.Extra)
                {
                    cost += 0.5m;
                }

                if (Tomato == Quantity.Extra)
                {
                    cost += 0.5m;
                }

                return cost;
            }
        }

        //This is for checking what subclass an enitiy is. There can be a problem with this if it is an NHibernate proxy.
        public virtual new IPizza Self
        {
            get { return this; }
        }
    }
}
