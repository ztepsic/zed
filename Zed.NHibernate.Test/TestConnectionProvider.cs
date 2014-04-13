using System.Data;
using System.Data.SQLite;
using NHibernate.Connection;

namespace Zed.NHibernate.Test {
    class TestConnectionProvider : ConnectionProvider {

        private static SQLiteConnection connection = null;

        /// <summary>
        /// Get an open <see cref="T:System.Data.IDbConnection"/>.
        /// </summary>
        /// <returns>
        /// An open <see cref="T:System.Data.IDbConnection"/>.
        /// </returns>
        public override IDbConnection GetConnection() {
            if (connection == null) {
                // new connection
                connection = new SQLiteConnection(ConnectionString);
            }

            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }

            return connection;

        }

        public override void CloseConnection(IDbConnection conn) {
            // ignore closing the connection
            // connection'll be closed by calling CloseDatabase by the and of TestFixture
        }

        public static void CloseDatabase() {
            if (connection != null) {
                connection.Close();
            }
        }
    }
}
