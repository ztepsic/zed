using System;

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
        /// Commits transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks transaction
        /// </summary>
        void Rollback();
    }
}
