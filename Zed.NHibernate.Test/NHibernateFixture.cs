﻿using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Zed.NHibernate.Test {
    public abstract class NHibernateFixture : BaseFixture {

        #region Members

        protected ISessionFactory SessionFactory { get { return TestNHibernateSessionProvider.SessionFactory; } }

        protected ISession Session { get { return SessionFactory.GetCurrentSession(); } }

        #endregion

        #region Constructors and Init

        #endregion

        #region Methods

        protected override void OnSetup() {
            SetupNHibernateSession();
            base.OnSetup();
        }

        protected override void OnTeardown() {
            TearDownNHibernateSession();
            base.OnTeardown();
        }

        protected void SetupNHibernateSession() {
            TestConnectionProvider.CloseDatabase();
            setupContextualSession();
            buildSchema();
        }

        protected void TearDownNHibernateSession() {
            tearDownContextualSession();
            TestConnectionProvider.CloseDatabase();
        }

        private void setupContextualSession() {
            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        private void tearDownContextualSession() {
            var sessionFactory = TestNHibernateSessionProvider.SessionFactory;
            var session = CurrentSessionContext.Unbind(sessionFactory);
            session.Close();
        }

        private void buildSchema() {
            var cfg = TestNHibernateSessionProvider.Configuration;
            var schemaExport = new SchemaExport(cfg);
            //schemaExport.Create(false, true);
            schemaExport.Execute(false, true, false, Session.Connection, null);
        }

        #endregion

    }
}
