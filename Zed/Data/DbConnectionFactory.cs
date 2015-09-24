using System;
using System.Data;
using System.Data.Common;

namespace Zed.Data {
    /// <summary>
    /// Ado.Net connection factory
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory {

        #region Fields and Properties

        /// <summary>
        /// Database connection create function
        /// </summary>
        private readonly Func<DbConnection> dbConnectionCreateFunc;

        /// <summary>
        /// Current database connection
        /// </summary>
        private DecoratedDbConnection currentConnection;

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates an Ado.Net connection factory based on provided create function
        /// </summary>
        /// <param name="dbConnectionCreateFunc"></param>
        public DbConnectionFactory(Func<DbConnection> dbConnectionCreateFunc) {
            this.dbConnectionCreateFunc = dbConnectionCreateFunc;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates new database connection.
        /// Returned connection is not open.
        /// </summary>
        /// <returns>New database connection</returns>
        protected DecoratedDbConnection Create() {
            return new DecoratedDbConnection(dbConnectionCreateFunc());
        }

        /// <summary>
        /// Creates and opens new database connection
        /// </summary>
        /// <returns>Created and opened new database connection</returns>
        public DecoratedDbConnection Open() {
            if (currentConnection != null && currentConnection.State != ConnectionState.Closed) {
                throw new InvalidOperationException("Current connection is active and not closed.");
            }

            currentConnection = Create();
            currentConnection.Open();
            return currentConnection;
        }


        /// <summary>
        /// Obtains the current connection.
        /// </summary>
        /// <returns>The current connection.</returns>
        public DecoratedDbConnection GetCurrentConnection() { return currentConnection; }

        /// <summary>
        /// Unbinds current connection
        /// </summary>
        /// <returns>Unbinded connection</returns>
        public DecoratedDbConnection UnbindCurrentConnection() {
            var dbConnection = currentConnection;
            currentConnection = null;
            return dbConnection;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">A Boolean that indicates whether the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).</param>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (currentConnection != null) {
                    currentConnection.Close();
                    currentConnection.Dispose();
                }
            }
        }

        #endregion

    }
}
