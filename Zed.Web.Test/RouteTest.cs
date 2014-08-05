using System.Web;
using System.Web.Routing;
using Zed.Web.Routes;

namespace Zed.Web.Test {
    /// <summary>
    /// Class provides set of methods for testing routes
    /// </summary>
    public static class RouteTest {

        #region Methods

        /// <summary>
        /// Method which checks that a provided URL does match with provieded routes and
        /// that segment variables extracted from matched URL correspond to expected controler,
        /// action and other route values.
        /// </summary>
        /// <param name="url">URL to test. URL must be prefixed with tilda (~) character.</param>
        /// <param name="routes">Collection of routes to match from.</param>
        /// <param name="expectedActionName">Excepted value for the action segment variable.</param>
        /// <param name="expectedControllerName">Expected value for the controler segment variable.</param>
        /// <param name="expectedRouteValues">Object that contains the expceted values for any additional segment variables.</param>
        /// <param name="httpMethod">Http method.</param>
        /// <returns>True if provieded URL does match with provided routes, false otherwise.</returns>
        public static bool RouteMatch(string url, RouteCollection routes, string expectedActionName, string expectedControllerName, 
            object expectedRouteValues = null, string httpMethod = "GET") {
            HttpContextBase httpContext = MockedHttpContextFactory.CreateHttpContext(url, httpMethod);
            RouteData routeDataResult = routes.GetRouteData(httpContext);

            return routeDataResult != null
                && routeDataResult.MatchWith(expectedActionName, expectedControllerName, expectedRouteValues);
        }


        /// <summary>
        /// Method which checks that a provided URL does not match with provided routes.
        /// </summary>
        /// <param name="url">URL to test. URL must be prefixed with tilda (~) character.</param>
        /// <param name="routes">Collection of routes to match from.</param>
        /// <returns>True if proviede URL does not work on provieded routes, false otherwise.</returns>
        public static bool RouteNotMatch(string url, RouteCollection routes) {
            HttpContextBase httpContext = MockedHttpContextFactory.CreateHttpContext(url);
            RouteData routeDataResult = routes.GetRouteData(httpContext);

            return (routeDataResult == null || routeDataResult.Route == null);
        }

        #endregion

    }
}
