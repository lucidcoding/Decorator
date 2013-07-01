namespace Decorator.Domain.Entities
{
    public class PepperoniDecorator : ToppingDecorator
    {
        public virtual bool ExtraSpicy { get; set; }

        public PepperoniDecorator()
        {
        }

        public PepperoniDecorator(IPizza basePizza, bool extraSpicy)
            : base(basePizza)
        {
            ExtraSpicy = extraSpicy;
        }

        public override decimal Cost
        {
            get
            {
                return base.Cost + 0.5m;
            }
        }
    }
}
