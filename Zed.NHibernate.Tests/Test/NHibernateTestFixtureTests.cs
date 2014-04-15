using System.Data.SQLite;
using NUnit.Framework;
using Zed.NHibernate.Test;

namespace Zed.NHibernate.Tests.Test {
    [TestFixture]
    public class NHibernateTestFixtureTests : NHibernateTestFixture {
        public NHibernateTestFixtureTests() : base(connString => new SQLiteConnection(connString)) { }

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
