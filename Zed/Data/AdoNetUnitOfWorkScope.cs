using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Zed.Transaction;

namespace Zed.Data {
    /// <summary>
    /// Ado.Net Unit Of Work scope
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    internal class AdoNetUnitOfWorkScope : IUnitOfWorkScope {

        #region Fields and Properties

        /// <summary>
        /// Database connection factory
        /// </summary>
        private readonly IDbConnectionFactory dbConnectionFactory;

        /// <summary>
        /// Gets database connection factory
        /// </summary>
        protected IDbConnectionFactory DbConnectionFactory => dbConnectionFactory;

        /// <summary>
        /// Gets current database connection
        /// </summary>
        protected DecoratedDbConnection DbConnection => dbConnectionFactory.GetCurrentConnection();

        /// <summary>
        /// Gets Ado.Net transaction
        /// </summary>
        protected IDbTransaction DbTransaction { get; private set; }

        /// <summary>
        /// Indicates if transaction is created
        /// </summary>
        private bool isTransactionCreated;

        /// <summary>
        /// Indicates if transaction scope is completed
        /// </summary>
        private bool isScopeCompleted;

        /// <summary>
        /// An indicator if transaction is active or not
        /// </summary>
        /// <returns></returns>
        public bool IsTransactionActive => DbConnection != null && DbConnection.IsTransactionActive;

        /// <summary>
        /// An indication if implicit transactions are enabled
        /// </summary>
        private readonly bool isImplicitTransactionsEnabled;

        /// <summary>
        /// Gets an indication if implicit transactions are enabled
        /// </summary>
        public bool IsImplicitTransactionsEnabled => isImplicitTransactionsEnabled;

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates an instance of Ado.Net unit of work scope
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory</param>
        /// <param name="isImplicitTransactionsEnabled">An indication if implicit transactions are enabled. Default is false.</param>
        public AdoNetUnitOfWorkScope(IDbConnectionFactory dbConnectionFactory, bool isImplicitTransactionsEnabled = false) {
            if (dbConnectionFactory != null) {
                this.dbConnectionFactory = dbConnectionFactory;
            } else {
                throw new ArgumentNullException(nameof(dbConnectionFactory));
            }

            this.isImplicitTransactionsEnabled = isImplicitTransactionsEnabled;

        }

        #endregion

        #region Members

        /// <summary>
        /// Begins/starts with transaction
        /// </summary>
        public virtual void BeginTransaction() {
            if (!IsTransactionActive) {
                isTransactionCreated = true;
                DbTransaction = DbConnection.BeginTransaction();
            }
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="BeginTransaction"/>.
        /// Begins/starts with transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual async Task BeginTransactionAsync(CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            BeginTransaction();
            await Task.CompletedTask.ConfigureAwait(false);
        }


        /// <summary>
        /// Commits transaction
        /// </summary>
        public virtual void Commit() {
            isScopeCompleted = true;
            if (isTransactionCreated) {
                DbTransaction.Commit();

                if (isImplicitTransactionsEnabled) {
                    isScopeCompleted = false;
                    BeginTransaction();
                }
            }
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="Commit"/>.
        /// Commits transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            Commit();
            await Task.CompletedTask.ConfigureAwait(false);
        }

        /// <summary>
        /// Rollbacks transaction
        /// </summary>
        public virtual void Rollback() {
            isScopeCompleted = true;
            if (isTransactionCreated) {
                DbTransaction.Rollback();

                if (isImplicitTransactionsEnabled) {
                    isScopeCompleted = false;
                    BeginTransaction();
                }
            }
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="Rollback"/>.
        /// Rollbacks transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RollbackAsync(CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            Rollback();
            await Task.CompletedTask.ConfigureAwait(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (!isScopeCompleted && DbConnection.IsTransactionActive) {
                    Rollback();
                }

                if (isTransactionCreated) {
                    DbTransaction?.Dispose();
                }
            }
        }

        #endregion

    }
}
