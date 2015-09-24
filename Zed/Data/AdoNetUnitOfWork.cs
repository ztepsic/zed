using System;
using System.Data;
using Zed.Transaction;

namespace Zed.Data {
    /// <summary>
    /// NHibernate unit of work
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    public class AdoNetUnitOfWork : IUnitOfWork {

        #region Fields and Properties

        /// <summary>
        /// Database connection factory
        /// </summary>
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly Func<IUnitOfWorkScope> rootScopeFactory;
        private readonly Func<IUnitOfWorkScope> dependentScopeFactory;

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates Ado.Net Unit of Work with default root and dependent scopes
        /// <param name="dbConnectionFactory">Database connection factory</param>
        /// </summary>
        public AdoNetUnitOfWork(IDbConnectionFactory dbConnectionFactory)
            : this(dbConnectionFactory,
            () => new AdoNetUnitOfWorkRootScope(dbConnectionFactory),
            () => new AdoNetUnitOfWorkScope(dbConnectionFactory)) { }

        /// <summary>
        /// Creates Ado.Net Unit of Work
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory</param>
        /// <param name="rootScopeFactory">Root transaction scope</param>
        /// <param name="dependentScopeFactory">Dependant transaction scope</param>
        public AdoNetUnitOfWork(IDbConnectionFactory dbConnectionFactory, Func<IUnitOfWorkScope> rootScopeFactory, Func<IUnitOfWorkScope> dependentScopeFactory) {
            if (dbConnectionFactory != null) {
                this.dbConnectionFactory = dbConnectionFactory;
            } else {
                throw new ArgumentNullException("dbConnectionFactory");
            }

            this.rootScopeFactory = rootScopeFactory;
            this.dependentScopeFactory = dependentScopeFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts unit of work scope
        /// </summary>
        /// <returns>Unit of work scope</returns>
        public IUnitOfWorkScope Start() {
            IUnitOfWorkScope scope = dbConnectionFactory.GetCurrentConnection() == null || dbConnectionFactory.GetCurrentConnection().State == ConnectionState.Closed
                ? rootScopeFactory()
                : dependentScopeFactory();

            scope.BeginTransaction();
            return scope;
        }

        #endregion

    }
}
