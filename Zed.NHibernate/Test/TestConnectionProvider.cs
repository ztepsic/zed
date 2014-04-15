using System;
using System.Data;
using NHibernate.Connection;

namespace Zed.NHibernate.Test {
    /// <summary>
    /// SQLite test connection provider
    /// </summary>
    class TestConnectionProvider : ConnectionProvider {

        private static IDbConnection connection = null;

        /// <summary>
        /// Create connection func
        /// </summary>
        public static Func<string, IDbConnection> CreateConnectionFunc { get; set; }

        /// <summary>
        /// Get an open <see cref="T:System.Data.IDbConnection"/>.
        /// </summary>
        /// <returns>
        /// An open <see cref="T:System.Data.IDbConnection"/>.
        /// </returns>
        public override IDbConnection GetConnection() {
            if (connection == null) {
                // new connection
                connection = CreateConnectionFunc.Invoke(ConnectionString);
            }

            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }

            return connection;

        }

        /// <summary>
        /// Close database connection
        /// </summary>
        /// <param name="conn"></param>
        public override void CloseConnection(IDbConnection conn) {
            // ignore closing the connection
            // connection'll be closed by calling CloseDatabase by the and of TestFixture
        }

        /// <summary>
        /// Close database
        /// </summary>
        public static void CloseDatabase() {
            if (connection != null) {
                connection.Close();
            }
        }
    }
}
