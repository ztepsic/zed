﻿using System;
using NHibernate;
using NHibernate.Cfg;

namespace Zed.NHibernate {
    /// <summary>
    /// NHibernate session provider
    /// </summary>
    public static class NHibernateSessionProvider {

        #region Fields and Properties

        /// <summary>
        /// NHibernate configuration
        /// </summary>
        private static readonly Configuration configuration;

        /// <summary>
        /// Gets NHibernate configuration
        /// </summary>
        public static Configuration Configuration { get { return configuration; } }

        /// <summary>
        /// Session factory
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Gets session factory
        /// </summary>
        public static ISessionFactory SessionFactory {
            get { return sessionFactory ?? (sessionFactory = configuration.BuildSessionFactory()); }
        }

        /// <summary>
        /// Gets the current session
        /// </summary>
        public static ISession CurrentSession { get { return sessionFactory.GetCurrentSession(); } }

        #endregion

        #region Constructors and Init

        static NHibernateSessionProvider() {
            configuration = new Configuration();
        }

        /// <summary>
        /// Initialize NHibernate session provider with NHibernate configuration
        /// </summary>
        /// <param name="configFunc">NHibernate configuration function</param>
        public static void Init(Func<Configuration, Configuration> configFunc) { configFunc(configuration); }

        #endregion

        #region Methods

        #endregion

    }
}
