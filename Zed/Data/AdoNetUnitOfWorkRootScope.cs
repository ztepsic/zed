using System.Data;

namespace Zed.Data {
    /// <summary>
    /// NHibernate Unit of Work Root Scope
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
        public AdoNetUnitOfWorkRootScope(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

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
