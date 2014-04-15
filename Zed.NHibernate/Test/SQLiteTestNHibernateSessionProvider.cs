using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using Environment = NHibernate.Cfg.Environment;

namespace Zed.NHibernate.Test {
    /// <summary>
    /// SQLite test NHibernate session provider
    /// </summary>
    static class SQLiteTestNHibernateSessionProvider {

        #region Fields and Properties

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        private static readonly Configuration configuration;

        /// <summary>
        /// Gets NHibernate configuration
        /// </summary>
        public static Configuration Configuration { get { return configuration; } }

        private static readonly ISessionFactory sessionFactory;

        /// <summary>
        /// Gets NHibernate session factory
        /// </summary>
        public static ISessionFactory SessionFactory { get { return sessionFactory; } }

        #endregion

        #region Constructors and Init

        static SQLiteTestNHibernateSessionProvider() {
            configuration = new Configuration()
                .DataBaseIntegration(db => {
                    db.Dialect<SQLiteDialect>();
                    db.Driver<SQLite20Driver>();
                    db.ConnectionProvider<TestConnectionProvider>();
                    db.ConnectionString = CONNECTION_STRING;
                })
                .SetProperty(Environment.CurrentSessionContextClass, "thread_static");

            var props = configuration.Properties;
            if (props.ContainsKey(Environment.ConnectionStringName)) {
                props.Remove(Environment.ConnectionStringName);
            }

            sessionFactory = configuration.BuildSessionFactory();
        }

        #endregion

        #region Methods
        #endregion

    }
}
