using System.Threading;
using System.Threading.Tasks;

namespace Zed.Transaction {
    /// <summary>
    /// Manages the creation and initialization of unit of work instances (<see cref="IUnitOfWork"/>).
    /// This interface acts as a factory for starting new transactional scopes and provides configuration
    /// for transaction behavior such as implicit transactions. Each call to <see cref="Start"/> or 
    /// <see cref="StartAsync"/> creates a new unit of work with an active transaction ready for use.
    /// </summary>
    /// <remarks>
    /// Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/
    /// Where IUnitOfWorkManager is called IUnitOfWork in the article.
    /// The manager handles the coordination of multiple nested or sequential units of work,
    /// ensuring proper transaction isolation and resource management.
    /// </remarks>
    public interface IUnitOfWorkManager {

        /// <summary>
        /// An indication if implicit transactions are enabled
        /// </summary>
        bool IsImplicitTransactionsEnabled { get; }

        /// <summary>
        /// Starts unit of work scope
        /// </summary>
        /// <returns>Unit of work scope</returns>
        IUnitOfWork Start();

        /// <summary>
        /// Starts async unit of work scope
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Unit of work scope</returns>
        Task<IUnitOfWork> StartAsync(CancellationToken cancellationToken = default);

    }
}
