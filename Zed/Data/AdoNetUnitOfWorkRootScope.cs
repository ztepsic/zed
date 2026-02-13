using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Zed.Data {
    /// <summary>
    /// Ado.Net Unit of Work Root Scope
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    class AdoNetUnitOfWorkRootScope : AdoNetUnitOfWorkScope {

        #region Fields and Properties

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates Ado.Net unit of work root scope
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory</param>
        /// <param name="isImplicitTransactionsEnabled">An indication if implicit transactions are enabled. Default is false.</param>
        public AdoNetUnitOfWorkRootScope(IDbConnectionFactory dbConnectionFactory, bool isImplicitTransactionsEnabled = false)
            : base(dbConnectionFactory, isImplicitTransactionsEnabled) { }

        #endregion

        #region Methods

        /// <summary>
        /// Commits transaction
        /// </summary>
        public override void BeginTransaction() {
            if (DbConnectionFactory.GetCurrentConnection() == null ||
                DbConnectionFactory.GetCurrentConnection().State == ConnectionState.Closed) {
                DbConnectionFactory.Open();
            }

            base.BeginTransaction();
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="BeginTransaction"/>.
        /// Begins/starts with transaction
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task BeginTransactionAsync(CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();

            if (DbConnectionFactory.GetCurrentConnection() == null ||
                DbConnectionFactory.GetCurrentConnection().State == ConnectionState.Closed) {
                await DbConnectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
            }

            await base.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) {
                var dbConnection = DbConnectionFactory.GetCurrentConnection();
                if (dbConnection != null) {
                    DbConnectionFactory.UnbindCurrentConnection();
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        #endregion

    }
}
