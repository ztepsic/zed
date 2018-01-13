using System.Threading;
using System.Threading.Tasks;

namespace Zed.Transaction {
    /// <summary>
    /// Unit of work
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    public interface IUnitOfWork {

        /// <summary>
        /// An indication if implicit transactions are enabled
        /// </summary>
        bool IsImplicitTransactionsEnabled { get; }

        /// <summary>
        /// Starts unit of work scope
        /// </summary>
        /// <returns>Unit of work scope</returns>
        IUnitOfWorkScope Start();

        /// <summary>
        /// Starts async unit of work scope
        /// </summary>
        /// <returns>Unit of work scope</returns>
        Task<IUnitOfWorkScope> StartAsync();

        /// <summary>
        /// Starts async unit of work scope
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Unit of work scope</returns>
        Task<IUnitOfWorkScope> StartAsync(CancellationToken cancellationToken);

    }
}
