using System;

namespace Zed.Data {
    /// <summary>
    /// Database connection factory interface
    /// </summary>
    public interface IDbConnectionFactory : IDisposable {
        /// <summary>
        /// Creates and opens a new database connection
        /// </summary>
        /// <returns></returns>
        DecoratedDbConnection Open();

        /// <summary>
        /// Obtains the current connection.
        /// </summary>
        /// <returns>The current connection.</returns>
        DecoratedDbConnection GetCurrentConnection();

        /// <summary>
        /// Unbinds current connection
        /// </summary>
        /// <returns>Unbinded connection</returns>
        DecoratedDbConnection UnbindCurrentConnection();
    }
}
