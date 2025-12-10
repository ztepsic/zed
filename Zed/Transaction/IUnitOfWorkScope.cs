using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zed.Transaction {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    public interface IUnitOfWorkScope : IDisposable {
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
        /// This is the asynchronous version of <see cref="IUnitOfWorkScope.BeginTransaction()"/>.
        /// Begins/starts with transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This is the asynchronous version of <see cref="IUnitOfWorkScope.Commit()"/>.
        /// Commits transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This is the asynchronous version of <see cref="IUnitOfWorkScope.Rollback()"/>.
        /// Rollbacks transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);

    }
}
