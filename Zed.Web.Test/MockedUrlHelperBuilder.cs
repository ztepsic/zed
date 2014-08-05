using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zed.Web.Test {
    /// <summary>
    /// Class that builds mocked url helper.
    /// </summary>
    public class MockedUrlHelperBuilder {

        #region Fields and Properties

        /// <summary>
        /// Gets or Sets http context
        /// </summary>
        public HttpContextBase HttpContext { get; set; }

        /// <summary>
        /// Gets route data
        /// </summary>
        public RouteData RouteData { get; set; }

        private readonly RouteCollection routes;

        /// <summary>
        /// Gets routes
        /// </summary>
        public RouteCollection Routes { get { return routes; } }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates mocked url helper builder
        /// </summary>
        public MockedUrlHelperBuilder() : this(new RouteCollection()){ }

        /// <summary>
        /// Creates mocked url helper builder
        /// </summary>
        /// <param name="routes">Routes</param>
        public MockedUrlHelperBuilder(RouteCollection routes) {
            this.routes = routes;
            RouteData = new RouteData();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns building result - mocked url helper
        /// </summary>
        /// <returns>Mocked url helper</returns>
        public UrlHelper GetResult() {
            return new UrlHelper(new RequestContext(HttpContext, RouteData), routes);
        }

        #endregion

    }
}
