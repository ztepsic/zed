using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zed.Transaction {
    /// <summary>
    /// Represents a unit of work that maintains a transactional boundary for database operations.
    /// This interface provides transaction lifecycle management including beginning, committing, and rolling back transactions.
    /// Implements IDisposable and IAsyncDisposable to ensure proper cleanup of database resources and automatic rollback of uncommitted transactions.
    /// </summary>
    /// <remarks>
    /// Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/
    /// Where IUnitOfWork is called IUnitOfWorkScope in the article.
    /// The unit of work pattern coordinates the writing out of changes and resolves concurrency problems.
    /// Use <see cref="IUnitOfWorkManager"/> to handle the coordination of multiple nested or sequential units of work.
    /// </remarks>
    public interface IUnitOfWork : IDisposable, IAsyncDisposable {
        /// <summary>
        /// Begins/starts with transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// An indicator if transaction is active or not
        /// </summary>
        /// <returns></returns>
        bool IsTransactionActive { get; }

        /// <summary>
        /// An indication if implicit transactions are enabled
        /// </summary>
        bool IsImplicitTransactionsEnabled { get; }

        /// <summary>
        /// Commits transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks transaction
        /// </summary>
        void Rollback();

        /// <summary>
        /// This is the asynchronous version of <see cref="IUnitOfWork.BeginTransaction()"/>.
        /// Begins/starts with transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This is the asynchronous version of <see cref="IUnitOfWork.Commit()"/>.
        /// Commits transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This is the asynchronous version of <see cref="IUnitOfWork.Rollback()"/>.
        /// Rollbacks transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);

    }
}
