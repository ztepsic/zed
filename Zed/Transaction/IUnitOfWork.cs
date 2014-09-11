namespace Zed.Transaction {
    /// <summary>
    /// Unit of work
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    public interface IUnitOfWork {
        /// <summary>
        /// Starts unit of work scope
        /// </summary>
        /// <returns>Unit of work scope</returns>
        IUnitOfWorkScope Start();
    }
}
