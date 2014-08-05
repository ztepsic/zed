using System;
using System.Web.Mvc;
using Zed.Core.Transaction;

namespace Zed.Web.Transaction {
    /// <summary>
    /// ASP.NET MVC Unit of Work action filter attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UnitOfWorkAttribute : ActionFilterAttribute {

        #region Fields and Properties

        /// <summary>
        /// Order of attribute in filter attribute stack.
        /// The number needs to be as high as possible because we want to delay
        /// the session and transaction opening to the last moment.
        /// </summary>
        private const int ORDER_OF_ATTRIBUTE_IN_STACK_OF_ATTRIBUTES = 100;

        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Current unit of work scope
        /// </summary>
        private IUnitOfWorkScope currentUnitOfWorkScope;

        /// <summary>
        /// Gets or Sets indication should we rollback on model state error
        /// </summary>
        public bool RollbackOnModelStateError { get; set; }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates Unit of Work attribute instance
        /// </summary>
        public UnitOfWorkAttribute(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
            Order = ORDER_OF_ATTRIBUTE_IN_STACK_OF_ATTRIBUTES;
            RollbackOnModelStateError = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The method begins with transaction
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            currentUnitOfWorkScope = unitOfWork.Start();
        }

        /// <summary>
        /// The method commits or rollbacks ongoing transaction.
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            if (currentUnitOfWorkScope != null) {
                try {
                    if ((filterContext.Exception != null) && (!filterContext.ExceptionHandled) || shouldRollback(filterContext)) {
                        currentUnitOfWorkScope.Rollback();
                    } else {
                        currentUnitOfWorkScope.Commit();
                    }
                } catch (Exception) {
                    currentUnitOfWorkScope.Rollback();
                } finally {
                    currentUnitOfWorkScope.Dispose();
                }
            }
        }


        private bool shouldRollback(ControllerContext filterContext) {
            return RollbackOnModelStateError && !filterContext.Controller.ViewData.ModelState.IsValid;
        }

        #endregion

    }

}
