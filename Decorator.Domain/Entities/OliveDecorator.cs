namespace Decorator.Domain.Entities
{
    public class OliveDecorator : ToppingDecorator
    {
        public virtual OliveColour Colour { get; set; }

        public OliveDecorator()
        {
        }

        public OliveDecorator(IPizza basePizza, OliveColour colour) : base(basePizza)
        {
            Colour = colour;
        }

        public override decimal Cost
        {
            get
            {
                return base.Cost + (Colour == OliveColour.Green ? 0.60m : 0.7m);
            }
        }
    }
}
