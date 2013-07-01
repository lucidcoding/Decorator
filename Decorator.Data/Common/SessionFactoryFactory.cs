using NHibernate;
using NHibernate.Cfg;

namespace Decorator.Data.Common
{
    public static class SessionFactoryFactory
    {
        public static ISessionFactory GetSessionFactory()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(SessionFactoryFactory).Assembly);
            var sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory;
        }
    }
}
