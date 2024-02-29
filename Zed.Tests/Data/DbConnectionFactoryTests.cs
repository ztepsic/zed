using System;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using Xunit;
using Zed.Data;

namespace Zed.Tests.Data {
    
    public class DbConnectionFactoryTests {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        public DbConnectionFactoryTests() {

        }

        [Fact]
        public void Ctor_DbConnectionFactory_Created() {
            // Arrange

            // Act
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Assert
            Assert.NotNull(dbConnectionFactory);
        }

        [Fact]
        public void Open_DbConnection_CreatedAndOpenedDbConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Act
            var dbConnection = dbConnectionFactory.Open();

            // Assert
            Assert.NotNull(dbConnection);
            Assert.Equal(ConnectionState.Open, dbConnection.State);
        }

        [Fact]
        public async Task OpenAsync_DbConnection_CreatedAndOpenedDbConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Act
            var dbConnection = await dbConnectionFactory.OpenAsync();

            // Assert
            Assert.NotNull(dbConnection);
            Assert.Equal(ConnectionState.Open, dbConnection.State);
        }

        [Fact]
        public void Open_CurrentDbConnectionExists_ThrownException() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));

            // Act and Assert
            dbConnectionFactory.Open();
            Assert.Throws<InvalidOperationException>(() => dbConnectionFactory.Open());
        }

        [Fact]
        public void GetCurrentConnection_ReturnsCurrentConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));
            var dbConnection = dbConnectionFactory.Open();

            // Act
            var currentConnection = dbConnectionFactory.GetCurrentConnection();

            // Assert
            Assert.NotNull(currentConnection);
            Assert.Equal(dbConnection, currentConnection);
        }

        [Fact]
        public void Unbind_CurrentDbConnection_UnbindedDbConnection() {
            // Arrange
            var dbConnectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));
            dbConnectionFactory.Open();
            var currentConnection = dbConnectionFactory.GetCurrentConnection();

            // Act
            var unbindedDbConnection = dbConnectionFactory.UnbindCurrentConnection();

            // Assert
            Assert.Null(dbConnectionFactory.GetCurrentConnection());
            Assert.Equal(currentConnection, unbindedDbConnection);
        }


    }
}
