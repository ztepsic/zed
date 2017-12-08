using System;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using NUnit.Framework;
using Zed.Data;

namespace Zed.Tests.Data {
    [TestFixture]
    public class DbConnectionFactoryTests {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";
        

        [SetUp]
        public void SetUp() { }

        [TearDown]
        public void TearDown() { }

        [Test]
        public void Ctor_DbConnectionFactory_Created() {
            // Arrange

            // Act
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Assert
            Assert.IsNotNull(dbConnectionFactory);
        }

        [Test]
        public void Open_DbConnection_CreatedAndOpenedDbConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Act
            var dbConnection = dbConnectionFactory.Open();

            // Assert
            Assert.IsNotNull(dbConnection);
            Assert.AreEqual(ConnectionState.Open, dbConnection.State);
        }

        [Test]
        public async Task OpenAsync_DbConnection_CreatedAndOpenedDbConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Act
            var dbConnection = await dbConnectionFactory.OpenAsync();

            // Assert
            Assert.IsNotNull(dbConnection);
            Assert.AreEqual(ConnectionState.Open, dbConnection.State);
        }

        [Test]
        public void Open_CurrentDbConnectionExists_ThrownException() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Act and Assert
            dbConnectionFactory.Open();
            Assert.Throws<InvalidOperationException>(() => dbConnectionFactory.Open());
        }

        [Test]
        public void GetCurrentConnection_ReturnsCurrentConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));
            var dbConnection = dbConnectionFactory.Open();

            // Act
            var currentConnection = dbConnectionFactory.GetCurrentConnection();

            // Assert
            Assert.IsNotNull(currentConnection);
            Assert.AreEqual(dbConnection, currentConnection);
        }

        [Test]
        public void Unbind_CurrentDbConnection_UnbindedDbConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));
            dbConnectionFactory.Open();
            var currentConnection = dbConnectionFactory.GetCurrentConnection();

            // Act
            var unbindedDbConnection = dbConnectionFactory.UnbindCurrentConnection();

            // Assert
            Assert.IsNull(dbConnectionFactory.GetCurrentConnection());
            Assert.AreEqual(currentConnection, unbindedDbConnection);
        }


    }
}
