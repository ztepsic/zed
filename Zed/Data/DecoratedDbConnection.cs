using System;
using System.Data;

namespace Zed.Data {
    /// <summary>
    /// Decorator for the database connection class, exposing additional info like it's transaction.
    /// </summary>
    public class DecoratedDbConnection : IDbConnection {

        #region Fields and Properties

        /// <summary>
        /// Database connection
        /// </summary>
        private readonly IDbConnection connection;

        /// <summary>
        /// Gets database connection
        /// </summary>
        public IDbConnection Connection { get { return connection; } }

        /// <summary>
        /// Gets a local transaction
        /// </summary>
        public virtual IDbTransaction Transaction { get; private set; }

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

        protected DecoratedDbConnection() { }

        /// <summary>
        /// Creates a database connection
        /// </summary>
        /// <param name="connection">Database connection for decoration</param>
        public DecoratedDbConnection(IDbConnection connection) {
            if (connection != null) {
                this.connection = connection;
            } else {
                throw new ArgumentNullException("connection");
            }
            
        }

        #endregion

        #region Methods

        #region IDbConnection Members

        /// <summary>
        /// Begins a database transaction with the specified <see cref="IsolationLevel"/> value.
        /// </summary>
        /// <param name="il">One of the IsolationLevel values. </param>
        /// <remarks>
        /// Once the transaction has completed, you must explicitly commit or roll back
        /// the transaction by using the Commit or Rollback methods.
        /// </remarks>
        /// <returns>An object representing the new transaction.</returns>
        public virtual IDbTransaction BeginTransaction(IsolationLevel il) {
            Transaction = connection.BeginTransaction(il);
            return Transaction;
        }

        /// <summary>
        /// Begins a database transaction.
        /// </summary>
        /// <remarks>
        /// Once the transaction has completed, you must explicitly commit or roll back
        /// the transaction by using the Commit or Rollback methods.
        /// </remarks>
        /// <returns>An object representing the new transaction.</returns>
        public virtual IDbTransaction BeginTransaction() {
            if (IsTransactionActive) throw new InvalidOperationException("Parallel transactions are not supported.");
            Transaction = connection.BeginTransaction();
            return Transaction;
        }

        /// <summary>
        /// Changes the current database for an open Connection object.
        /// </summary>
        /// <param name="databaseName">The name of the database to use in place of the current database. </param>
        public virtual void ChangeDatabase(string databaseName) {
            connection.ChangeDatabase(databaseName);
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        /// <remarks>
        /// The Close method rolls back any pending transactions. It then releases
        /// the connection to the connection pool, or closes the connection if connection pooling is disabled.
        /// 
        /// An application can call Close more than one time without generating an exception.
        /// </remarks>
        public virtual void Close() {
            Transaction = null;
            connection.Close();
        }

        /// <summary>
        /// Gets or sets the string used to open a database.
        /// </summary>
        public virtual string ConnectionString {
            get {
                return connection.ConnectionString;
            }
            set {
                connection.ConnectionString = value;
            }
        }

        /// <summary>
        /// Gets the time to wait while trying to establish a connection before
        /// terminating the attempt and generating an error.
        /// </summary>
        public virtual int ConnectionTimeout {
            get { return connection.ConnectionTimeout; }
        }

        /// <summary>
        /// Creates and returns a Command object associated with the connection.
        /// </summary>
        /// <returns>A Command object associated with the connection.</returns>
        public virtual IDbCommand CreateCommand() {
            return connection.CreateCommand();
        }

        /// <summary>
        /// Gets the name of the current database or the database to be used after a connection is opened.
        /// </summary>
        public virtual string Database {
            get { return connection.Database; }
        }

        /// <summary>
        /// Opens a database connection with the settings specified by the ConnectionString
        /// property of the provider-specific Connection object.
        /// </summary>
        public virtual void Open() {
            connection.Open();
            Transaction = null;
        }

        /// <summary>
        /// Gets the current state of the connection.
        /// </summary>
        public virtual ConnectionState State {
            get { return connection.State; }
        }

        /// <summary>
        /// Dispose the underlying connection.
        /// </summary>
        /// <param name="disposing">false if pre-empted from a <c>finalizer</c></param>
        protected virtual void Dispose(bool disposing) {
            if (disposing && connection != null) {
                connection.Dispose();
            }
            //connection = null;
            
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

    }
}
