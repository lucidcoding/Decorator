using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Decorator.Data.Common;
using Decorator.Domain.Entities;
using NUnit.Framework;

namespace Decorator.Data.Tests
{
    [TestFixture]
    public class PizzaTests
    {
        private IPizza _pizza;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            new SqlDatabaseInitialiser().InitialiseDatabase();

            Guid id;

            using (var session = SessionFactoryFactory.GetSessionFactory().OpenSession())
            {
                var pizza = Pizza.Create(10, Quantity.Extra, Quantity.Regular);
                id = pizza.Id.Value;

                using (var transaction = session.BeginTransaction())
                {
                    session.Save(pizza);
                    transaction.Commit();
                }
            }

            using (var session = SessionFactoryFactory.GetSessionFactory().OpenSession())
            {
                _pizza = session.Get<IPizza>(id);
            }
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenSizeIsCorrect()
        {
            Assert.That(_pizza.Size, Is.EqualTo(10));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenCheeseIsCorrect()
        {
            Assert.That(_pizza.Cheese, Is.EqualTo(Quantity.Extra));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenTomatoIsCorrect()
        {
            Assert.That(_pizza.Tomato, Is.EqualTo(Quantity.Regular));
        }

        [Test]
        public void GivenPizzaIsSavedWhenPizzaIsLoadedThenCostIsCorrect()
        {
            Assert.That(_pizza.Cost, Is.EqualTo(7.49m));
        }
    }
}
