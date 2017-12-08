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
        /// This is the asynchronous version of <see cref="BeginTransaction"/>.
        /// Begins/starts with transaction
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// This is the asynchronous version of <see cref="BeginTransaction"/>.
        /// Begins/starts with transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Commits transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks transaction
        /// </summary>
        void Rollback();
    }
}
