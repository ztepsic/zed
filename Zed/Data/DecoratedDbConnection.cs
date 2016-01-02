using System;
using System.Data;
using System.Data.Common;

namespace Zed.Data {
    /// <summary>
    /// Decorator for the database connection class, exposing additional info like it's transaction.
    /// </summary>
    public class DecoratedDbConnection : DbConnection {

        #region Fields and Properties

        /// <summary>
        /// Database connection
        /// </summary>
        private readonly DbConnection connection;

        /// <summary>
        /// Gets database connection
        /// </summary>
        public DbConnection Connection { get { return connection; } }

        /// <summary>
        /// Gets a local transaction
        /// </summary>
        public virtual DbTransaction Transaction { get; private set; }

        /// <summary>
        /// An indicator which tells if connection has a transaction
        /// </summary>
        public bool HasTransaction {
            get { return Transaction != null; }
        }


        /// <summary>
        /// An indicator which tells if current transaction is active
        /// </summary>
        public bool IsTransactionActive {
            get {
                return HasTransaction &&
                       Transaction.Connection != null;
            }
        }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates a database connection
        /// </summary>
        /// <param name="connection">Database connection for decoration</param>
        public DecoratedDbConnection(DbConnection connection) {
            if (connection != null) {
                this.connection = connection;
            } else {
                throw new ArgumentNullException(nameof(connection));
            }
            
        }

        #endregion

        #region Methods

        #region DbConnection Members


        /// <summary>
        /// Gets the name of the current database or the database to be used after a connection is opened.
        /// </summary>
        public override string Database { get { return connection.Database; } }

        /// <summary>
        /// Gets or sets the string used to open a database.
        /// </summary>
        public override string ConnectionString {
            get { return connection.ConnectionString; }
            set { connection.ConnectionString = value; }
        }

        /// <summary>
        /// Gets the time to wait while trying to establish a connection before
        /// terminating the attempt and generating an error.
        /// </summary>
        public override int ConnectionTimeout { get { return connection.ConnectionTimeout; } }

        /// <summary>
        /// Gets the current state of the connection.
        /// </summary>
        public override ConnectionState State { get { return connection.State; } }

        /// <summary>
        /// Gets the name of the database server to which to connect.
        /// </summary>
        public override string DataSource { get { return connection.DataSource; } }

        /// <summary>
        /// Gets a string that represents the version of the server to which the object is connected.
        /// </summary>
        public override string ServerVersion { get { return connection.ServerVersion; } }

        /// <summary>
        /// Begins a database transaction with the specified <see cref="IsolationLevel"/> value.
        /// </summary>
        /// <remarks>
        /// Once the transaction has completed, you must explicitly commit or roll back
        /// the transaction by using the Commit or Rollback methods.
        /// </remarks>
        /// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
        /// <returns>An object representing the new transaction.</returns>
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) {
            if (IsTransactionActive) throw new InvalidOperationException("Parallel transactions are not supported.");
            Transaction = connection.BeginTransaction(isolationLevel);
            return Transaction;
        }

        /// <summary>
        /// Changes the current database for an open Connection object.
        /// </summary>
        /// <param name="databaseName">The name of the database to use in place of the current database. </param>
        public override void ChangeDatabase(string databaseName) { connection.ChangeDatabase(databaseName); }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        /// <remarks>
        /// The Close method rolls back any pending transactions. It then releases
        /// the connection to the connection pool, or closes the connection if connection pooling is disabled.
        /// 
        /// An application can call Close more than one time without generating an exception.
        /// </remarks>
        public override void Close() {
            Transaction = null;
            connection.Close();
        }

        /// <summary>
        /// Creates and returns a Command object associated with the connection.
        /// </summary>
        /// <returns>A Command object associated with the connection.</returns>
        protected override DbCommand CreateDbCommand() { return connection.CreateCommand(); }

        /// <summary>
        /// Opens a database connection with the settings specified by the ConnectionString
        /// property of the provider-specific Connection object.
        /// </summary>
        public override void Open() {
            connection.Open();
            Transaction = null;
        }

        #endregion

        /// <summary>
        /// Dispose the underlying connection.
        /// </summary>
        /// <param name="disposing">false if pre-empted from a <c>finalizer</c></param>
        protected override void Dispose(bool disposing) {
            if (disposing && connection != null) {
                connection.Dispose();
                
            }
            //connection = null;

            base.Dispose(disposing);

        }

        #endregion

    }
}
