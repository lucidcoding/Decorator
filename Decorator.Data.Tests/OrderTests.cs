using System;
using Decorator.Data.Common;
using Decorator.Domain.Entities;
using NHibernate;
using NUnit.Framework;

namespace Decorator.Data.Tests
{
    [TestFixture]
    public class OrderTests
    {
        private Order _order;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            new SqlDatabaseInitialiser().InitialiseDatabase();
            Guid id;

            using (var session = SessionFactoryFactory.GetSessionFactory().OpenSession())
            {
                var order = Order.Create("Barry Blue", "14 Blue Street, Blueville");
                order.Add(Pizza.Create(12, Quantity.Regular, Quantity.Extra));
                order.Add(new PepperoniDecorator(Pizza.Create(10, Quantity.Extra, Quantity.Regular), true));
                order.Add(new OliveDecorator(new PepperoniDecorator(Pizza.Create(10, Quantity.Regular, Quantity.Regular), false), OliveColour.Black));
                id = order.Id.Value;

                using (var transaction = session.BeginTransaction())
                {
                    session.Save(order);
                    transaction.Commit();
                }
            }

            using (var session = SessionFactoryFactory.GetSessionFactory().OpenSession())
            {
                _order = session
                    .QueryOver<Order>()
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                NHibernateUtil.Initialize(_order.Items); //Ensure everything is loaded that we need.
                NHibernateUtil.Initialize(_order.Items[0].Cheese);
                NHibernateUtil.Initialize(_order.Items[1].Cheese);
                NHibernateUtil.Initialize(_order.Items[2].Cheese);
            }
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenHasCorrectNumberOfItems()
        {
            Assert.That(_order.Items.Count, Is.EqualTo(3));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenFirstItemIsOfCorrectType()
        {
            Assert.That(_order.Items[0].Self, Is.InstanceOf<Pizza>());
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenSecondItemIsOfCorrectType()
        {
            Assert.That(_order.Items[1].Self, Is.InstanceOf<PepperoniDecorator>());
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenThirdItemIsOfCorrectType()
        {
            Assert.That(_order.Items[2].Self, Is.InstanceOf<OliveDecorator>());
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenFirstIsCorrectSize()
        {
            Assert.That(_order.Items[0].Size, Is.EqualTo(12));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenSecondIsCorrectSize()
        {
            Assert.That(_order.Items[1].Size, Is.EqualTo(10));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenThirdIsCorrectSize()
        {
            Assert.That(_order.Items[2].Size, Is.EqualTo(10));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenFirstHasCorrectCheese()
        {
            Assert.That(_order.Items[0].Cheese, Is.EqualTo(Quantity.Regular));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenSecondHasCorrectCheese()
        {
            Assert.That(_order.Items[1].Cheese, Is.EqualTo(Quantity.Extra));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenThirdHasCorrectCheese()
        {
            Assert.That(_order.Items[2].Cheese, Is.EqualTo(Quantity.Regular));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenFirstHasCorrectTomato()
        {
            Assert.That(_order.Items[0].Tomato, Is.EqualTo(Quantity.Extra));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenSecondHasCorrectTomato()
        {
            Assert.That(_order.Items[1].Tomato, Is.EqualTo(Quantity.Regular));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenThirdHasCorrectTomato()
        {
            Assert.That(_order.Items[2].Tomato, Is.EqualTo(Quantity.Regular));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenFirstHasCorrectCost()
        {
            Assert.That(_order.Items[0].Cost, Is.EqualTo(8.49m));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenSecondHasCorrectCost()
        {
            Assert.That(_order.Items[1].Cost, Is.EqualTo(7.99m));
        }

        [Test]
        public void GivenOrderIsSavedWhenOrderIsLoadedThenThirdHasCorrectCost()
        {
            Assert.That(_order.Items[2].Cost, Is.EqualTo(8.19m));
        }
    }
}
