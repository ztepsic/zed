﻿using System;
using NHibernate;
using NHibernate.Context;
using Zed.Core.Transaction;

namespace Zed.NHibernate {
    /// <summary>
    /// NHibernate unit of work
    /// </summary>
    /// <remarks>Based on article: http://www.planetgeek.ch/2012/05/05/what-is-that-all-about-the-repository-anti-pattern/ </remarks>
    public class NHibernateUnitOfWork : IUnitOfWork {

        #region Fields and Properties

        private readonly ISessionFactory sessionFactory;
        private readonly Func<IUnitOfWorkScope> rootScopeFactory;
        private readonly Func<IUnitOfWorkScope> dependentScopeFactory;

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates NHibernate Unit of Work with default root and dependent scopes
        /// </summary>
        /// <param name="sessionFactory">NHibernate session factory</param>
        public NHibernateUnitOfWork(ISessionFactory sessionFactory) 
            : this(sessionFactory,
            () => new NHibernateUnitOfWorkRootScope(sessionFactory),
            () => new NHibernateUnitOfWorkScope(sessionFactory)) { }

        /// <summary>
        /// Creates NHibernate Unit of Work
        /// </summary>
        /// <param name="sessionFactory">NHibernate session factory</param>
        /// <param name="rootScopeFactory">Root transaction scope</param>
        /// <param name="dependentScopeFactory">Dependant transaction scope</param>
        public NHibernateUnitOfWork(ISessionFactory sessionFactory, Func<IUnitOfWorkScope> rootScopeFactory, Func<IUnitOfWorkScope> dependentScopeFactory) {
            this.sessionFactory = sessionFactory;
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
            IUnitOfWorkScope scope = !CurrentSessionContext.HasBind(sessionFactory)
                ? rootScopeFactory()
                : dependentScopeFactory();

            scope.BeginTransaction();
            return scope;
        }

        #endregion

    }
}
