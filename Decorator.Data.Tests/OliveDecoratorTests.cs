using System;
using Decorator.Data.Common;
using Decorator.Domain.Entities;
using NHibernate;
using NUnit.Framework;

namespace Decorator.Data.Tests
{
    [TestFixture]
    public class OliveDecoratorTests
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
                var olivePizza = new OliveDecorator(pizza, OliveColour.Green);
                id = olivePizza.Id.Value;

                using (var transaction = session.BeginTransaction())
                {
                    session.Save(olivePizza);
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
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenColourIsCorrect()
        {
            Assert.That((_pizza as OliveDecorator).Colour, Is.EqualTo(OliveColour.Green));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenCostIsCorrect()
        {
            Assert.That(_pizza.Cost, Is.EqualTo(9.09m));
        }
    }
}
