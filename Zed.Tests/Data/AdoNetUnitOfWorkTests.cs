using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Moq;
using NUnit.Framework;
using Zed.Data;
using Zed.Transaction;

namespace Zed.Tests.Data {
    [TestFixture]
    public class AdoNetUnitOfWorkTests {

        public class WrappedDecoratedDbConnection : DecoratedDbConnection {
            protected WrappedDecoratedDbConnection() : base(new SQLiteConnection(CONNECTION_STRING)) { }

            public WrappedDecoratedDbConnection(DbConnection dbConnection) : base(dbConnection) { }

        }

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";
        private IDbConnectionFactory connectionFactory;

        [SetUp]
        public void SetUp() {
            connectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));
            var connection = connectionFactory.Open();

            const string sql = "create table Tag (name nvarchar(20))";

            var command = new SQLiteCommand(sql, connection.Connection as SQLiteConnection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        [Test]
        public void Ctor_AdoNetUnitOfWork_Created() {
            // Arrange

            // Act
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Assert
            Assert.IsNotNull(unitOfWork);

        }

        [Test]
        public void Start_AdoNetUnitOfWork_UnitOfWorkCreated() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.IsNotNull(unitOfWorkScope);
        }

        [Test]
        public void Start_ConnectionIsNullOrClosed_CreatesUnitOfWorkRootScopeAndOpensConnection() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.AreEqual("AdoNetUnitOfWorkRootScope", unitOfWorkScope.GetType().Name);
            Assert.AreEqual(ConnectionState.Open , connectionFactory.GetCurrentConnection().State);
        }

        [Test]
        public void Start_SecondCallToStartWhileFirstUoWIsStillActive_CreatesDependentUnitOfWorkScope() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkRootScope = unitOfWork.Start();
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.AreEqual("AdoNetUnitOfWorkRootScope", unitOfWorkRootScope.GetType().Name);
            Assert.AreEqual("AdoNetUnitOfWorkScope", unitOfWorkScope.GetType().Name);
            
        }

        [Test]
        public void Start_Scope_TransactionStarted() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.IsTrue(unitOfWorkScope.IsTransactionActive);
        }

        [Test]
        public void Start_CalledBeginTransactionOnUnitOfWorkScope_NoEffects() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            var transaction1 = connectionFactory.GetCurrentConnection().Transaction;

            unitOfWorkScope.BeginTransaction();
            var transaction2 = connectionFactory.GetCurrentConnection().Transaction;

            // Assert
            Assert.AreSame(transaction1, transaction2);
        }

        [Test]
        public void Start_OnTwoDependedScopes_OneTransaction() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkRootScope = unitOfWork.Start();
            var transaction1 = connectionFactory.GetCurrentConnection().Transaction;
            var unitOfWorkScope = unitOfWork.Start();
            var transaction2 = connectionFactory.GetCurrentConnection().Transaction;

            // Assert
            Assert.AreNotEqual(unitOfWorkRootScope, unitOfWorkScope);
            Assert.AreSame(transaction1, transaction2);
        }


        [Test]
        public void Start_OnCommitedScope_CreatesDependedUnitOfWorkScopeWithNewTransaction() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);
            var unitOfWorkScope = unitOfWork.Start();
            var transaction1 = connectionFactory.GetCurrentConnection().Transaction;
            unitOfWorkScope.Commit();

            // Act
            var unitOfWorkScope2 = unitOfWork.Start();
            var transaction2 = connectionFactory.GetCurrentConnection().Transaction;


            // Assert
            Assert.AreEqual("AdoNetUnitOfWorkRootScope", unitOfWorkScope.GetType().Name);
            Assert.AreEqual("AdoNetUnitOfWorkScope", unitOfWorkScope2.GetType().Name);

            Assert.AreNotSame(transaction1, transaction2);

            Assert.AreEqual(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Test]
        public void Commit_IUnitOfWorkScope_CommitedTransactionAndConnectionStillOpen() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Commit();

            // Assert
            Assert.IsFalse(unitOfWorkScope.IsTransactionActive);
            Assert.AreEqual(ConnectionState.Open , connectionFactory.GetCurrentConnection().State);
        }

        [Test]
        public void Commit_IUnitOfWorkScope__WithImplicitTransactions_AfterCommitedNewTransactionIsStarted() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory, true);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Commit();

            // Assert
            Assert.IsTrue(unitOfWorkScope.IsTransactionActive);
            Assert.AreEqual(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }


        [Test]
        public void Commit_IUnitOfWorkScope_InSameUnitOfWorkAfterFirstCommitCanStartNewTransaction() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            var transaction1 = connectionFactory.GetCurrentConnection().Transaction;
            unitOfWorkScope.Commit();

            unitOfWorkScope.BeginTransaction();
            var transaction2 = connectionFactory.GetCurrentConnection().Transaction;

            // Assert
            Assert.AreNotSame(transaction1, transaction2);
            Assert.IsTrue(unitOfWorkScope.IsTransactionActive);
            Assert.AreEqual(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Test]
        public void Commit_IUnitOfWorkScope_TwoDependentScopesOnlyRootScopeReallyCommits() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            var unitOfWorkScope2 = unitOfWork.Start();
            unitOfWorkScope2.Commit();

            var isTransactionFromScope2Active = unitOfWorkScope2.IsTransactionActive;
            var isTransactionFromScope1BeforeItsCommitActive = unitOfWorkScope.IsTransactionActive;

            unitOfWorkScope.Commit();

            var isTransactionFromScope1AfterItsCommitActive = unitOfWorkScope.IsTransactionActive;

            // Assert
            Assert.IsTrue(isTransactionFromScope2Active);
            Assert.IsTrue(isTransactionFromScope1BeforeItsCommitActive);
            Assert.IsFalse(isTransactionFromScope1AfterItsCommitActive);
            Assert.AreEqual(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Test]
        [Ignore("System.NotSupportedException : Invalid setup on a non-virtual member x => x.BeginTransaction()")]
        public void Commit_IUnitOfWorkScope_CommitCalledOnTransaction() {
            // Arrange
            Mock<DbTransaction> transactionMock = new Mock<DbTransaction>();

            Mock<WrappedDecoratedDbConnection> connectionMock = new Mock<WrappedDecoratedDbConnection>();
            connectionMock.Setup(x => x.Transaction).Returns(transactionMock.Object);
            connectionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            connectionMock.Setup(x => x.State).Returns(ConnectionState.Closed);

            Mock<IDbConnectionFactory> connectionFactoryMock = new Mock<IDbConnectionFactory>();
            connectionFactoryMock.Setup(x => x.GetCurrentConnection()).Returns(connectionMock.Object);

            var unitOfWork = new AdoNetUnitOfWork(connectionFactoryMock.Object);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Commit();

            // Assert
            transactionMock.Verify(x => x.Commit());
        }


        [Test]
        public void Rollback_IUnitOfWorkScope_RollbackedTransactionAndConnectionStillOpen() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Rollback();

            // Assert
            Assert.IsFalse(unitOfWorkScope.IsTransactionActive);
            Assert.AreEqual(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Test]
        public void Rollback_IUnitOfWorkScope_WithImplicitTransactions_AfterRollbackedTransactionNewTransationIsStarted() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory, true);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Rollback();

            // Assert
            Assert.IsTrue(unitOfWorkScope.IsTransactionActive);
            Assert.AreEqual(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Test]
        public void Dispose_IUnitOfWorkScope_WithImplicitTransactions_ActiveTransactionIsRolledback() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory, true);

            // Act
            IUnitOfWorkScope unitOfWorkScope = null;
            using (unitOfWorkScope = unitOfWork.Start()) {
                
            }


            // Assert
            Assert.IsFalse(unitOfWorkScope.IsTransactionActive);
            Assert.IsNull(connectionFactory.GetCurrentConnection());
        }

        [Test]
        [Ignore("System.NotSupportedException : Invalid setup on a non-virtual member x => x.BeginTransaction()")]
        public void Rollback_IUnitOfWorkScope_RollbackCalledOnTransaction() {
            // Arrange
            Mock<DbTransaction> transactionMock = new Mock<DbTransaction>();

            Mock<DecoratedDbConnection> connectionMock = new Mock<DecoratedDbConnection>();
            connectionMock.Setup(x => x.Transaction).Returns(transactionMock.Object);
            connectionMock.Setup(x => x.BeginTransaction()).Returns(transactionMock.Object);
            connectionMock.Setup(x => x.State).Returns(ConnectionState.Closed);

            Mock<IDbConnectionFactory> connectionFactoryMock = new Mock<IDbConnectionFactory>();
            connectionFactoryMock.Setup(x => x.GetCurrentConnection()).Returns(connectionMock.Object);

            var unitOfWork = new AdoNetUnitOfWork(connectionFactoryMock.Object);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Rollback();

            // Assert
            transactionMock.Verify(x => x.Rollback());
        }

    }
}
