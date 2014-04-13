using log4net;
using NHibernate;
using NHibernate.Cfg;

namespace Zed.NHibernate {
    /// <summary>
    /// NHibernate session provider
    /// </summary>
    public static class NHibernateSessionProvider {

        #region Fields and Properties

        /// <summary>
        /// Session factory
        /// </summary>
        private static readonly ISessionFactory sessionFactory;

        /// <summary>
        /// Gets session factory
        /// </summary>
        public static ISessionFactory SessionFactory { get { return sessionFactory; } }

        /// <summary>
        /// Gets the current session
        /// </summary>
        public static ISession CurrentSession { get { return sessionFactory.GetCurrentSession(); } }

        #endregion

        #region Constructors and Init

        static NHibernateSessionProvider() {
            ILog log = LogManager.GetLogger(typeof (NHibernateSessionProvider));
            log.Debug("Trying to create NHibernate session factory.");
            try {
                Configuration nHConfig = new Configuration().Configure();
                sessionFactory = nHConfig.BuildSessionFactory();
            } catch (HibernateException ex) {
                log.Error("Can't create NHibernate session factory.", ex);
                throw;
            }
        }

        /// <summary>
        /// Initialize NHibernate session provider.
        /// Method ensures that static constructor of NHibernateSessionProvider is called
        /// </summary>
        public static void Init() { }

        #endregion

        #region Methods

        #endregion

    }
}
