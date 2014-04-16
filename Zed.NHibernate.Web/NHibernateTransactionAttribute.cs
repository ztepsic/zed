using System;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;

namespace Zed.NHibernate.Web {
    /// <summary>
    /// ASP.NET MVC NHibernate transaction action filter attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class NHibernateTransactionAttribute : ActionFilterAttribute {

        #region Fields and Properties

        /// <summary>
        /// Order of attribute in filter attribute stack.
        /// The number needs to be as high as possible because we want to delay
        /// the session and transaction opening to the last moment.
        /// </summary>
        private const int ORDER_OF_ATTRIBUTE_IN_STACK_OF_ATTRIBUTES = 100;

        /// <summary>
        /// NHibernate session factory.
        /// Only one session factory should be created during request
        /// </summary>
        private readonly ISessionFactory sessionFactory;

        /// <summary>
        /// Gets or Sets indication should we rollback on model state error
        /// </summary>
        public bool RollbackOnModelStateError { get; set; }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Configures NHibernate
        /// </summary>
        static NHibernateTransactionAttribute() { NHibernateSessionProvider.Configuration.Configure(); }

        /// <summary>
        /// Creates NHibernate transaction attribute instance
        /// </summary>
        public NHibernateTransactionAttribute() {
            Order = ORDER_OF_ATTRIBUTE_IN_STACK_OF_ATTRIBUTES;
            sessionFactory = NHibernateSessionProvider.SessionFactory;
            RollbackOnModelStateError = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The method opens NHibernate session and begins transaction
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            ISession session = sessionFactory.OpenSession();
            session.BeginTransaction();
            CurrentSessionContext.Bind(session);
        }

        /// <summary>
        /// The method commits or rollbacks ongoing transaction and finally closes NHibernate session.
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            ISession session = CurrentSessionContext.Unbind(sessionFactory);
            if (session != null) {
                try {
                    var transaction = session.Transaction;
                    if (transaction != null && transaction.IsActive) {
                        if ((filterContext.Exception != null) && (!filterContext.ExceptionHandled) || shouldRollback(filterContext)) {
                            session.Transaction.Rollback();
                        } else {
                            session.Transaction.Commit();
                        }
                    }
                } catch (HibernateException) {
                    session.Transaction.Rollback();
                } finally {
                    session.Close();
                }
            }
        }


        private bool shouldRollback(ControllerContext filterContext) {
            return RollbackOnModelStateError && !filterContext.Controller.ViewData.ModelState.IsValid;
        }

        #endregion

    }
}
