using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Zed.Transaction;

namespace Zed.Data {
    /// <summary>
    /// AdoNet unit of work manager
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    public class AdoNetUnitOfWorkManager : IUnitOfWorkManager {

        #region Fields and Properties

        /// <summary>
        /// Database connection factory
        /// </summary>
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly Func<IUnitOfWork> rootScopeFactory;
        private readonly Func<IUnitOfWork> dependentScopeFactory;

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
        /// Creates Ado.Net Unit of Work with default root and dependent scopes
        /// <param name="dbConnectionFactory">Database connection factory</param>
        /// <param name="isImplicitTransactionsEnabled">An indication if implicit transactions are enabled. Default is false.</param>
        /// </summary>
        public AdoNetUnitOfWorkManager(IDbConnectionFactory dbConnectionFactory, bool isImplicitTransactionsEnabled = false)
            : this(dbConnectionFactory,
            () => new AdoNetUnitOfWorkRootScope(dbConnectionFactory, isImplicitTransactionsEnabled),
            () => new AdoNetUnitOfWorkScope(dbConnectionFactory, isImplicitTransactionsEnabled),
            isImplicitTransactionsEnabled) { }

        /// <summary>
        /// Creates Ado.Net Unit of Work
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory</param>
        /// <param name="rootScopeFactory">Root transaction scope</param>
        /// <param name="dependentScopeFactory">Dependant transaction scope</param>
        /// <param name="isImplicitTransactionsEnabled">An indication if implicit transactions are enabled. Default is false.</param>
        public AdoNetUnitOfWorkManager(IDbConnectionFactory dbConnectionFactory, Func<IUnitOfWork> rootScopeFactory, Func<IUnitOfWork> dependentScopeFactory, bool isImplicitTransactionsEnabled = false) {
            if (dbConnectionFactory != null) {
                this.dbConnectionFactory = dbConnectionFactory;
            } else {
                throw new ArgumentNullException("dbConnectionFactory");
            }

            this.rootScopeFactory = rootScopeFactory;
            this.dependentScopeFactory = dependentScopeFactory;
            this.isImplicitTransactionsEnabled = isImplicitTransactionsEnabled;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts unit of work scope
        /// </summary>
        /// <returns>Unit of work scope</returns>
        public IUnitOfWork Start() {
            IUnitOfWork scope = dbConnectionFactory.GetCurrentConnection() == null || dbConnectionFactory.GetCurrentConnection().State == ConnectionState.Closed
                ? rootScopeFactory()
                : dependentScopeFactory();

            //if (IsImplicitTransactionsEnabled) { scope.BeginTransaction(); }

            scope.BeginTransaction();

            return scope;
        }


        /// <summary>
        /// Starts async unit of work scope
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>Unit of work scope</returns>
        public async Task<IUnitOfWork> StartAsync(CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();

            IUnitOfWork scope = dbConnectionFactory.GetCurrentConnection() == null || dbConnectionFactory.GetCurrentConnection().State == ConnectionState.Closed
                ? rootScopeFactory()
                : dependentScopeFactory();

            if (IsImplicitTransactionsEnabled) {
                await scope.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            }
            return scope;
        }

        #endregion

    }
}
