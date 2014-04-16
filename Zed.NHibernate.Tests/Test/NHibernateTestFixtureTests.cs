using System.Data.SQLite;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NUnit.Framework;
using Zed.NHibernate.Test;

namespace Zed.NHibernate.Tests.Test {
    [TestFixture]
    public class NHibernateTestFixtureTests : NHibernateTestFixture {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        static NHibernateTestFixtureTests() {
            TestConnectionProvider.CreateConnectionFunc = connString => new SQLiteConnection(connString);
            Configuration.DataBaseIntegration(db => {
                db.Dialect<SQLiteDialect>();
                db.Driver<SQLite20Driver>();
                db.ConnectionProvider<TestConnectionProvider>();
                db.ConnectionString = CONNECTION_STRING;
            })
            .SetProperty(Environment.CurrentSessionContextClass, "thread_static");

            var configProperties = Configuration.Properties;
            if (configProperties.ContainsKey(Environment.ConnectionStringName)) {
                configProperties.Remove(Environment.ConnectionStringName);
            }
        }

        [TestFixtureSetUp]
        public void FixtureSetup() { OnFixtureSetup(); }

        [TestFixtureTearDown]
        public void FixtureTearDown() { OnFixtureTeardown(); }

        [SetUp]
        public void Setup() { OnSetup(); }

        [TearDown]
        public void TearDown() { OnTeardown(); }

        [Test]
        public void OpenSessionAndTransaction_TransactionRolledback() {
            // Arrange

            // Act
            using (var session = SessionFactory.OpenSession()) {
                using (var trx = session.BeginTransaction()) {
                    trx.Rollback();
                }
            }

            // Assert
        }

    }
}
