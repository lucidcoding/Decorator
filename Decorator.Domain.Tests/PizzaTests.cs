using Decorator.Domain.Entities;
using NUnit.Framework;

namespace Decorator.Domain.Tests
{
    [TestFixture]
    public class PizzaTests
    {
        [Test]
        public void Given10InchRegularCheeseAndTomatoWhenCostIsCalculatedThenCorrectValueIsReturned()
        {
            var pizza = Pizza.Create(10, Quantity.Regular, Quantity.Regular);
            Assert.That(pizza.Cost, Is.EqualTo(6.99m));
        }

        [Test]
        public void Given12InchExtraCheeseAndTomatoWhenCostIsCalculatedThenCorrectValueIsReturned()
        {
            var pizza = Pizza.Create(12, Quantity.Extra, Quantity.Extra);
            Assert.That(pizza.Cost, Is.EqualTo(8.99m));
        }

        [Test]
        public void Given10InchRegularCheeseAndTomatoWithPepperoniWhenCostIsCalculatedThenCorrectValueIsReturned()
        {
            var pizza = Pizza.Create(10, Quantity.Regular, Quantity.Regular);
            pizza = new PepperoniDecorator(pizza, true);
            Assert.That(pizza.Cost, Is.EqualTo(7.49m));
        }

        [Test]
        public void Given12InchExtraCheeseAndTomatoWithBlackOlivesWhenCostIsCalculatedThenCorrectValueIsReturned()
        {
            var pizza = Pizza.Create(12, Quantity.Extra, Quantity.Extra);
            pizza = new OliveDecorator(pizza, OliveColour.Black);
            Assert.That(pizza.Cost, Is.EqualTo(9.69m));
        }

        [Test]
        public void Given14InchExtraCheeseAndTomatoWithBlackOlivesAndPepperoniWhenCostIsCalculatedThenCorrectValueIsReturned()
        {
            var pizza = Pizza.Create(14, Quantity.Extra, Quantity.Extra);
            pizza = new OliveDecorator(pizza, OliveColour.Black);
            pizza = new PepperoniDecorator(pizza, true);
            Assert.That(pizza.Cost, Is.EqualTo(11.19m));
        }

        [Test]
        public void Given14InchExtraCheeseAndTomatoWithBlackOlivesAndPepperoniWhenSizeIsCalledThenOriginalValueIsRetained()
        {
            var pizza = Pizza.Create(14, Quantity.Extra, Quantity.Extra);
            pizza = new OliveDecorator(pizza, OliveColour.Black);
            pizza = new PepperoniDecorator(pizza, true);
            Assert.That(pizza.Size, Is.EqualTo(14));
        }

        [Test]
        public void Given14InchExtraCheeseAndTomatoWithBlackOlivesAndPepperoniWhenCheeseIsCalledThenOriginalValueIsRetained()
        {
            var pizza = Pizza.Create(14, Quantity.Extra, Quantity.Extra);
            pizza = new OliveDecorator(pizza, OliveColour.Black);
            pizza = new PepperoniDecorator(pizza, true);
            Assert.That(pizza.Cheese, Is.EqualTo(Quantity.Extra));
        }

        [Test]
        public void Given14InchExtraCheeseAndTomatoWithBlackOlivesAndPepperoniWhenTomatoIsCalledThenOriginalValueIsRetained()
        {
            var pizza = Pizza.Create(14, Quantity.Extra, Quantity.Extra);
            pizza = new OliveDecorator(pizza, OliveColour.Black);
            pizza = new PepperoniDecorator(pizza, true);
            Assert.That(pizza.Tomato, Is.EqualTo(Quantity.Extra));
        }

        [Test]
        public void Given14InchExtraCheeseAndTomatoWithBlackOlivesWhenOliveColourIsCalledThenCorrectValueIsReturned()
        {
            var pizza = Pizza.Create(14, Quantity.Extra, Quantity.Extra);
            pizza = new OliveDecorator(pizza, OliveColour.Black);
            Assert.That((pizza as OliveDecorator).Colour, Is.EqualTo(OliveColour.Black));
        }

        [Test]
        public void Given14InchExtraCheeseAndTomatoWithBlackOlivesAndPepperoniWhenExtraSpicyIsCalledThenCorrectValueIsReturned()
        {
            var pizza = Pizza.Create(14, Quantity.Extra, Quantity.Extra);
            pizza = new OliveDecorator(pizza, OliveColour.Black);
            pizza = new PepperoniDecorator(pizza, true);
            Assert.That((pizza as PepperoniDecorator).ExtraSpicy, Is.True);
        }
    }
}
