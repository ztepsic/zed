using System;
using log4net;
using NUnit.Framework;

namespace Zed.NHibernate.Test {
    public abstract class BaseFixture {

        #region Fields and Properties

        protected static ILog Log = new Func<ILog>(() => {
            log4net.Config.XmlConfigurator.Configure();
            return LogManager.GetLogger(typeof(BaseFixture));
        }).Invoke();


        #endregion

        #region Constructors and Init

        #endregion

        #region Methods

        protected virtual void OnFixtureSetup() { }
        protected virtual void OnFixtureTeardown() { }
        protected virtual void OnSetup() { }
        protected virtual void OnTeardown() { }


        [TestFixtureSetUp]
        public void FixtureSetup() { OnFixtureSetup(); }

        [TestFixtureTearDown]
        public void FixtureTearDown() { OnFixtureTeardown(); }

        [SetUp]
        public void Setup() { OnSetup(); }

        [TearDown]
        public void TearDown() { OnTeardown(); }

        #endregion

    }
}
