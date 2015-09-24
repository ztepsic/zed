using System;

namespace Zed.Data {
    /// <summary>
    /// Ado.Net abstract repository
    /// 
    /// Connection management (connection opening, closing) and transaction management (commits and rollbacks)
    /// are meant to be done outside of this repository instance.
    /// </summary>
    public abstract class AdoNetRepository {

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
        /// Gets current connection
        /// </summary>
        protected DecoratedDbConnection DbConnection { get { return dbConnectionFactory.GetCurrentConnection(); } }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates an instance of Ado.Net repository
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory</param>
        protected AdoNetRepository(IDbConnectionFactory dbConnectionFactory) {
            if (dbConnectionFactory == null) {
                throw new ArgumentNullException("dbConnectionFactory");
            }

            this.dbConnectionFactory = dbConnectionFactory;
        }

        #endregion

        #region Methods

        #endregion

    }
}
