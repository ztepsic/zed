using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading.Tasks;
using NUnit.Framework;
using Zed.Data;

namespace Zed.Tests.Data {
    [TestFixture]
    public class DecoratedDbConnectionTests {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";
        private DbConnection origDbConnection;

        [SetUp]
        public void SetUp() {
            origDbConnection = new SQLiteConnection(CONNECTION_STRING);
        }

        [TearDown]
        public void TearDown() {
            origDbConnection.Close();
        }

        [Test]
        public void Ctor_DbConnection_Created() {
            // Arrange

            // Act
            var dbConnection = new DecoratedDbConnection(origDbConnection);

            // Assert
            Assert.IsNotNull(dbConnection);
            Assert.AreEqual(ConnectionState.Closed, dbConnection.State);
            Assert.IsNull(dbConnection.Transaction);
        }

        [Test]
        public void Ctor_NullConnection_ExceptionThrown() {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => new DecoratedDbConnection(null));
        }

        [Test]
        public void Open_DbConnection_OpenedConnection() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);

            // Act
            dbConnection.Open();

            // Assert
            Assert.AreEqual(ConnectionState.Open, dbConnection.State);
            Assert.IsFalse(dbConnection.HasTransaction);
            Assert.IsFalse(dbConnection.IsTransactionActive);

            dbConnection.Close();
        }

        [Test]
        public async Task OpenAsync_DbConnection_OpenedConnection() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);

            // Act
            await dbConnection.OpenAsync();

            // Assert
            Assert.AreEqual(ConnectionState.Open, dbConnection.State);
            Assert.IsFalse(dbConnection.HasTransaction);
            Assert.IsFalse(dbConnection.IsTransactionActive);

            dbConnection.Close();
        }

        [Test]
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


        [Test]
        public void Close_DbConnection_ClosedConnection() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            dbConnection.Close();

            // Assert
            Assert.AreEqual(ConnectionState.Closed, dbConnection.State);
            Assert.IsNull(dbConnection.Transaction);
            Assert.IsFalse(dbConnection.HasTransaction);
            Assert.IsFalse(dbConnection.IsTransactionActive);
        }

        [Test]
        public void Close_OngoingTransaction_ClosedConnectionAndTransactionIsNull() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();

            // Act
            dbConnection.Close();

            // Assert
            Assert.AreEqual(ConnectionState.Closed, dbConnection.State);
            Assert.IsNull(dbConnection.Transaction);
        }

        [Test]
        public void BeginTransaction_DbConnection_InitiatedTransaction() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            dbConnection.BeginTransaction();

            // Assert
            Assert.IsNotNull(dbConnection.Transaction);
            Assert.AreEqual(dbConnection.Connection, dbConnection.Transaction.Connection);
            Assert.IsTrue(dbConnection.HasTransaction);
            Assert.IsTrue(dbConnection.IsTransactionActive);
        }

        [Test]
        public void BeginTransaction_MultipleCalls_ExceptionThrown() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            var tx1 = dbConnection.BeginTransaction();

            // Assert
            Assert.Throws<InvalidOperationException>(() => dbConnection.BeginTransaction());

        }

        [Test]
        public void HasTransaction_IfTransactionIsNotCreated_ConnectionDoesntHaveTransaction() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            var hasTransaction = dbConnection.HasTransaction;

            // Assert
            Assert.IsFalse(hasTransaction);

            dbConnection.Close();
        }

        [Test]
        public void HasTransaction_IfTransactionIsCreated_ConnectionHasTransaction() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();

            // Act
            var hasTransaction = dbConnection.HasTransaction;

            // Assert
            Assert.IsTrue(hasTransaction);

            dbConnection.Close();
        }

        [Test]
        public void IsTransactionActive_IfBeginTransactionWasCalled_TransactionIsActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.IsTrue(isTransactionActive);
        }

        [Test]
        public void IsTransactionActive_IfBeginTransactionWasNotCalled_TransactionIsNotActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.IsFalse(isTransactionActive);
        }

        [Test]
        public void IsTransactionActive_AfterCommit_TransactionIsNotActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();
            dbConnection.Transaction.Commit();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.IsNull(dbConnection.Transaction.Connection);
            Assert.IsFalse(isTransactionActive);
        }


        [Test]
        public void IsTransactionActive_AfterRollback_TransactionIsNotActive() {
            // Arrange
            var dbConnection = new DecoratedDbConnection(origDbConnection);
            dbConnection.Open();
            dbConnection.BeginTransaction();
            dbConnection.Transaction.Commit();

            // Act
            var isTransactionActive = dbConnection.IsTransactionActive;

            // Assert
            Assert.IsNull(dbConnection.Transaction.Connection);
            Assert.IsFalse(isTransactionActive);
        }

    }
}
