using Moq;
using Moq.Protected;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Xunit;
using Zed.Data;
using Zed.Transaction;

namespace Zed.Tests.Data {

    public class AdoNetUnitOfWorkTests {

        public class WrappedDecoratedDbConnection : DecoratedDbConnection {
            protected WrappedDecoratedDbConnection() : base(new SQLiteConnection(CONNECTION_STRING)) { }

            public WrappedDecoratedDbConnection(DbConnection dbConnection) : base(dbConnection) { }

        }

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";
        private IDbConnectionFactory connectionFactory;

        public AdoNetUnitOfWorkTests() {
            connectionFactory = new DbConnectionFactory(() => new SQLiteConnection(CONNECTION_STRING));
            var connection = connectionFactory.Open();

            const string sql = "create table Tag (name nvarchar(20))";

            var command = new SQLiteCommand(sql, connection.Connection as SQLiteConnection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        [Fact]
        public void Ctor_AdoNetUnitOfWork_Created() {
            // Arrange

            // Act
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Assert
            Assert.NotNull(unitOfWork);

        }

        [Fact]
        public void Start_AdoNetUnitOfWork_UnitOfWorkCreated() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.NotNull(unitOfWorkScope);
        }

        [Fact]
        public void Start_ConnectionIsNullOrClosed_CreatesUnitOfWorkRootScopeAndOpensConnection() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.Equal("AdoNetUnitOfWorkRootScope", unitOfWorkScope.GetType().Name);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Fact]
        public void Start_SecondCallToStartWhileFirstUoWIsStillActive_CreatesDependentUnitOfWorkScope() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkRootScope = unitOfWork.Start();
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.Equal("AdoNetUnitOfWorkRootScope", unitOfWorkRootScope.GetType().Name);
            Assert.Equal("AdoNetUnitOfWorkScope", unitOfWorkScope.GetType().Name);

        }

        [Fact]
        public void Start_Scope_TransactionStarted() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();

            // Assert
            Assert.True(unitOfWorkScope.IsTransactionActive);
        }

        [Fact]
        public void Start_CalledBeginTransactionOnUnitOfWorkScope_NoEffects() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            var transaction1 = connectionFactory.GetCurrentConnection().Transaction;

            unitOfWorkScope.BeginTransaction();
            var transaction2 = connectionFactory.GetCurrentConnection().Transaction;

            // Assert
            Assert.Same(transaction1, transaction2);
        }

        [Fact]
        public void Start_OnTwoDependedScopes_OneTransaction() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkRootScope = unitOfWork.Start();
            var transaction1 = connectionFactory.GetCurrentConnection().Transaction;
            var unitOfWorkScope = unitOfWork.Start();
            var transaction2 = connectionFactory.GetCurrentConnection().Transaction;

            // Assert
            Assert.NotEqual(unitOfWorkRootScope, unitOfWorkScope);
            Assert.Same(transaction1, transaction2);
        }


        [Fact]
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
            Assert.Equal("AdoNetUnitOfWorkRootScope", unitOfWorkScope.GetType().Name);
            Assert.Equal("AdoNetUnitOfWorkScope", unitOfWorkScope2.GetType().Name);

            Assert.NotSame(transaction1, transaction2);

            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Fact]
        public void Commit_IUnitOfWorkScope_CommitedTransactionAndConnectionStillOpen() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Commit();

            // Assert
            Assert.False(unitOfWorkScope.IsTransactionActive);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Fact]
        public void Commit_IUnitOfWorkScope__WithImplicitTransactions_AfterCommitedNewTransactionIsStarted() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory, true);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Commit();

            // Assert
            Assert.True(unitOfWorkScope.IsTransactionActive);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }


        [Fact]
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
            Assert.NotSame(transaction1, transaction2);
            Assert.True(unitOfWorkScope.IsTransactionActive);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Fact]
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
            Assert.True(isTransactionFromScope2Active);
            Assert.True(isTransactionFromScope1BeforeItsCommitActive);
            Assert.False(isTransactionFromScope1AfterItsCommitActive);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Fact]
        public void Commit_IUnitOfWorkScope_CommitCalledOnTransaction() {
            // Arrange
            var transactionMock = new Mock<DbTransaction>();

            var connectionMock = new Mock<WrappedDecoratedDbConnection>(MockBehavior.Strict);
            connectionMock
                .Setup(x => x.Transaction)
                .Returns(transactionMock.Object);

            connectionMock.Protected()
                .Setup<DbTransaction>("BeginDbTransaction", IsolationLevel.Unspecified)
                .Returns(transactionMock.Object);

            connectionMock
                .Setup(x => x.State)
                .Returns(ConnectionState.Closed);

            var connectionFactoryMock = new Mock<IDbConnectionFactory>();
            connectionFactoryMock
                .Setup(x => x.GetCurrentConnection())
                .Returns(connectionMock.Object);

            var unitOfWork = new AdoNetUnitOfWork(connectionFactoryMock.Object);

            // Act
            var t = connectionMock.Object.BeginTransaction();
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Commit();

            // Assert
            transactionMock.Verify(x => x.Commit());
        }


        [Fact]
        public void Rollback_IUnitOfWorkScope_RollbackedTransactionAndConnectionStillOpen() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Rollback();

            // Assert
            Assert.False(unitOfWorkScope.IsTransactionActive);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Fact]
        public void Rollback_IUnitOfWorkScope_WithImplicitTransactions_AfterRollbackedTransactionNewTransationIsStarted() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory, true);

            // Act
            var unitOfWorkScope = unitOfWork.Start();
            unitOfWorkScope.Rollback();

            // Assert
            Assert.True(unitOfWorkScope.IsTransactionActive);
            Assert.Equal(ConnectionState.Open, connectionFactory.GetCurrentConnection().State);
        }

        [Fact]
        public void Dispose_IUnitOfWorkScope_WithImplicitTransactions_ActiveTransactionIsRolledback() {
            // Arrange
            var unitOfWork = new AdoNetUnitOfWork(connectionFactory, true);

            // Act
            IUnitOfWorkScope unitOfWorkScope = null;
            using (unitOfWorkScope = unitOfWork.Start()) {

            }


            // Assert
            Assert.False(unitOfWorkScope.IsTransactionActive);
            Assert.Null(connectionFactory.GetCurrentConnection());
        }

        [Fact]
        public void Rollback_IUnitOfWorkScope_RollbackCalledOnTransaction() {
            // Arrange
            var transactionMock = new Mock<DbTransaction>();

            var connectionMock = new Mock<WrappedDecoratedDbConnection>(MockBehavior.Strict);
            connectionMock.Setup(x => x.Transaction).Returns(transactionMock.Object);
            connectionMock.Protected()
                .Setup<DbTransaction>("BeginDbTransaction", IsolationLevel.Unspecified)
                .Returns(transactionMock.Object);
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
