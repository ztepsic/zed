using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading.Tasks;
using Xunit;
using Zed.Data;

namespace Zed.Tests.Data {
    
    public class DecoratedDbConnectionTests : IDisposable {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";
        private DbConnection origDbConnection;
        private bool disposedValue;

        public DecoratedDbConnectionTests() {
            origDbConnection = new SQLiteConnection(CONNECTION_STRING);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    origDbConnection.Close();
                }

                disposedValue = true;
            }
        }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void Ctor_DbConnection_Created() {
            // Arrange

            // Act
            var dbConnection = new DecoratedDbConnection(origDbConnection);

            // Assert
            Assert.NotNull(dbConnection);
            Assert.Equal(ConnectionState.Closed, dbConnection.State);
            Assert.Null(dbConnection.Transaction);
        }

        [Fact]
        public void Ctor_NullConnection_ExceptionThrown() {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new DecoratedDbConnection(null));
        }

        [Fact]
        public void Open_DbConnection_OpenedConnection() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);

            // Act
            dbConnection.Open();

            // Assert
            Assert.Equal(ConnectionState.Open, dbConnection.State);
            Assert.False(dbConnection.HasTransaction);
            Assert.False(dbConnection.IsTransactionActive);

            dbConnection.Close();
        }

        [Fact]
        public async Task OpenAsync_DbConnection_OpenedConnection() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);

            // Act
            await dbConnection.OpenAsync();

            // Assert
            Assert.Equal(ConnectionState.Open, dbConnection.State);
            Assert.False(dbConnection.HasTransaction);
            Assert.False(dbConnection.IsTransactionActive);

            dbConnection.Close();
        }

        [Fact]
        public void Open_MultipleCalls_ExceptionThrown() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);

            // Act and Assert
            try {
                dbConnection.Open();
                Assert.Throws<InvalidOperationException>(() => dbConnection.Open());
            } finally {
                dbConnection.Close();
            }


        }


        [Fact]
        public void Close_DbConnection_ClosedConnection() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            dbConnection.Close();

            // Assert
            Assert.Equal(ConnectionState.Closed, dbConnection.State);
            Assert.Null(dbConnection.Transaction);
            Assert.False(dbConnection.HasTransaction);
            Assert.False(dbConnection.IsTransactionActive);
        }

        [Fact]
        public void Close_OngoingTransaction_ClosedConnectionAndTransactionIsNull() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();

            // Act
            dbConnection.Close();

            // Assert
            Assert.Equal(ConnectionState.Closed, dbConnection.State);
            Assert.Null(dbConnection.Transaction);
        }

        [Fact]
        public void BeginTransaction_DbConnection_InitiatedTransaction() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            dbConnection.BeginTransaction();

            // Assert
            Assert.NotNull(dbConnection.Transaction);
            Assert.Equal(dbConnection.Connection, dbConnection.Transaction.Connection);
            Assert.True(dbConnection.HasTransaction);
            Assert.True(dbConnection.IsTransactionActive);
        }

        [Fact]
        public void BeginTransaction_MultipleCalls_ExceptionThrown() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            var tx1 = dbConnection.BeginTransaction();

            // Assert
            Assert.Throws<InvalidOperationException>(() => dbConnection.BeginTransaction());

        }

        [Fact]
        public void HasTransaction_IfTransactionIsNotCreated_ConnectionDoesntHaveTransaction() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            var hasTransaction = dbConnection.HasTransaction;

            // Assert
            Assert.False(hasTransaction);

            dbConnection.Close();
        }

        [Fact]
        public void HasTransaction_IfTransactionIsCreated_ConnectionHasTransaction() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();

            // Act
            var hasTransaction = dbConnection.HasTransaction;

            // Assert
            Assert.True(hasTransaction);

            dbConnection.Close();
        }

        [Fact]
        public void IsTransactionActive_IfBeginTransactionWasCalled_TransactionIsActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.True(isTransactionActive);
        }

        [Fact]
        public void IsTransactionActive_IfBeginTransactionWasNotCalled_TransactionIsNotActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.False(isTransactionActive);
        }

        [Fact]
        public void IsTransactionActive_AfterCommit_TransactionIsNotActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();
            dbConnection.Transaction.Commit();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.Null(dbConnection.Transaction.Connection);
            Assert.False(isTransactionActive);
        }


        [Fact]
        public void IsTransactionActive_AfterRollback_TransactionIsNotActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();
            dbConnection.Transaction.Commit();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.Null(dbConnection.Transaction.Connection);
            Assert.False(isTransactionActive);
        }

    }
}
