using System;
using Decorator.Data.Common;
using Decorator.Domain.Entities;
using NHibernate;
using NUnit.Framework;

namespace Decorator.Data.Tests
{
    [TestFixture]
    public class OliveAndPepperoniDecoratorTests
    {
        private IPizza _pizza;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            new SqlDatabaseInitialiser().InitialiseDatabase();
            Guid id;

            using (var session = SessionFactoryFactory.GetSessionFactory().OpenSession())
            {
                var pizza = Pizza.Create(12, Quantity.Regular, Quantity.Extra);
                var pepperoniPizza = new PepperoniDecorator(pizza, true);
                var oliveAndPepperoniPizza = new OliveDecorator(pepperoniPizza, OliveColour.Black);
                id = oliveAndPepperoniPizza.Id.Value;

                using (var transaction = session.BeginTransaction())
                {
                    session.Save(oliveAndPepperoniPizza);
                    transaction.Commit();
                }
            }

            using (var session = SessionFactoryFactory.GetSessionFactory().OpenSession())
            {
                _pizza = session
                    .QueryOver<IPizza>()
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                NHibernateUtil.Initialize(_pizza.Cheese); //To lazy load base pizza.
            }
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenSizeIsCorrect()
        {
            Assert.That(_pizza.Size, Is.EqualTo(12));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenCheeseIsCorrect()
        {
            Assert.That(_pizza.Cheese, Is.EqualTo(Quantity.Regular));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenTomatoIsCorrect()
        {
            Assert.That(_pizza.Tomato, Is.EqualTo(Quantity.Extra));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenExtraSpicyIsCorrect()
        {
            Assert.That(((_pizza.Self as OliveDecorator).BasePizza.Self as PepperoniDecorator).ExtraSpicy, Is.True);
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenExtraOliveColourIsCorrect()
        {
            Assert.That((_pizza.Self as OliveDecorator).Colour, Is.EqualTo(OliveColour.Black));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenCostIsCorrect()
        {
            Assert.That(_pizza.Cost, Is.EqualTo(9.69m));
        }
    }
}
