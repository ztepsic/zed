using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zed.Data {
    /// <summary>
    /// Database connection factory interface
    /// </summary>
    public interface IDbConnectionFactory : IDisposable {
        /// <summary>
        /// Creates and opens a new database connection
        /// </summary>
        /// <returns>Created and opened new database connection</returns>
        DecoratedDbConnection Open();

        /// <summary>
        /// An asynchronous version of Open, which opens a database connection with the settings
        /// specified by the ConnectionString. 
        /// This method invokes the virtual method OpenAsync with CancellationToken.None.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<DecoratedDbConnection> OpenAsync();

        /// <summary>
        /// This is the asynchronous version of Open. Providers should override with an appropriate
        /// implementation. The cancellation token can optionally be honored.
        /// 
        /// The default implementation invokes the synchronous Open call and returns a completed task.
        /// The default implementation will return a cancelled task if passed an already cancelled 
        /// cancellationToken. 
        /// 
        /// Exceptions thrown by Open will be communicated via the returned Task Exception property.
        /// 
        /// Do not invoke other methods and properties of the DbConnection object until the returned
        /// Task is complete.
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<DecoratedDbConnection> OpenAsync(CancellationToken cancellationToken);

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
