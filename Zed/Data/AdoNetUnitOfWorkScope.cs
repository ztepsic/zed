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
    class AdoNetUnitOfWorkScope : IUnitOfWorkScope {

        #region Fields and Properties

        /// <summary>
        /// Database connection factory
        /// </summary>
        private readonly IDbConnectionFactory dbConnectionFactory;

        /// <summary>
        /// Gets database connection factory
        /// </summary>
        protected IDbConnectionFactory DbConnectionFactory { get { return dbConnectionFactory; } }

        /// <summary>
        /// Gets current database connection
        /// </summary>
        protected DecoratedDbConnection DbConnection { get { return dbConnectionFactory.GetCurrentConnection(); } }

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

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates an instance of Ado.Net unit of work scope
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory</param>
        public AdoNetUnitOfWorkScope(IDbConnectionFactory dbConnectionFactory) {
            if (dbConnectionFactory != null) {
                this.dbConnectionFactory = dbConnectionFactory;
            } else {
                throw new ArgumentNullException("dbConnectionFactory");
            }

        }

        #endregion

        #region Members

        /// <summary>
        /// Begins/starts with transaction
        /// </summary>
        public virtual void BeginTransaction() {
            if (!DbConnection.IsTransactionActive) {
                isTransactionCreated = true;
                DbTransaction = DbConnection.BeginTransaction();
            }
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="BeginTransaction"/>.
        /// This method invokes the virtual method <see cref="BeginTransactionAsync()"/> with CancellationToken.None.
        /// Begins/starts with transaction
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual async Task BeginTransactionAsync() {
            await BeginTransactionAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="BeginTransaction"/>.
        /// Begins/starts with transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual async Task BeginTransactionAsync(CancellationToken cancellationToken) {
            BeginTransaction();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Commits transaction
        /// </summary>
        public virtual void Commit() {
            isScopeCompleted = true;
            if (isTransactionCreated) {
                DbTransaction.Commit();
            }
        }

        /// <summary>
        /// Rollbacks transaction
        /// </summary>
        public virtual void Rollback() {
            isScopeCompleted = true;
            if (isTransactionCreated) {
                DbTransaction.Rollback();
            }
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

                if (isTransactionCreated && DbTransaction != null) {
                    DbTransaction.Dispose();
                }
            }
        }

        #endregion

    }
}
