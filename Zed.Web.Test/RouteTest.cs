using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Zed.Web.Test {
    /// <summary>
    /// Class provides set of methods for testing routes
    /// </summary>
    public static class RouteTest {

        #region Constants

        private const string ROUTE_VALUE_KEY_CONTROLLER = "controller";
        private const string ROUTE_VALUE_KEY_ACTION = "action";

        #endregion

        #region Methods

        /// <summary>
        /// Method which checks that a provided URL does match with provieded routes and
        /// that segment variables extracted from matched URL correspond to expected controler,
        /// action and other route values.
        /// </summary>
        /// <param name="url">URL to test</param>
        /// <param name="routes">Collection of routes to match from</param>
        /// <param name="expectedController">Expected value for the controler segment variable</param>
        /// <param name="expectedAction">Excepted value for the action segment variable</param>
        /// <param name="expectedRouteValues">Object that contains the expceted values for any additional segment variables</param>
        /// <param name="httpMethod">Http method</param>
        /// <returns>True if provieded URL does match with provided routes, false otherwise.</returns>
        public static bool RouteMatch(string url, RouteCollection routes, string expectedController, string expectedAction,
            object expectedRouteValues = null, string httpMethod = "GET") {
            HttpContextBase httpContext = MockedHttpContextFactory.CreateHttpContext(url, httpMethod);
            RouteData routeDataResult = routes.GetRouteData(httpContext);

            return routeDataResult != null
                   && RouteDataResultMatchWith(routeDataResult, expectedController, expectedAction, expectedRouteValues);
        }


        /// <summary>
        /// Method which checks that a provided URL does not match with provided routes.
        /// </summary>
        /// <param name="url">URL to test</param>
        /// <param name="routes">Collection of routes to match from</param>
        /// <returns>True if proviede URL does not work on provieded routes, false otherwise.</returns>
        public static bool RouteNotMatch(string url, RouteCollection routes) {
            HttpContextBase httpContext = MockedHttpContextFactory.CreateHttpContext(url);
            RouteData routeDataResult = routes.GetRouteData(httpContext);

            return (routeDataResult == null || routeDataResult.Route == null);
        }

        /// <summary>
        /// Method which compares the result obtained from the routing system with the expected segment
        /// variable values.
        /// </summary>
        /// <param name="routeDataResult">Result obtainded from the routeing system</param>
        /// <param name="expectedController">Expected controller name</param>
        /// <param name="expectedAction">Expected action name</param>
        /// <param name="expectedRouteValues">Expected other segment variable values</param>
        private static bool RouteDataResultMatchWith(RouteData routeDataResult, string expectedController, string expectedAction, object expectedRouteValues = null) {
            // helper function to compare two strings
            Func<object, object, bool> stringCompare = (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1.ToString(), v2.ToString()) == 0;

            // compare controller and action values
            bool result = stringCompare(routeDataResult.Values[ROUTE_VALUE_KEY_CONTROLLER], expectedController)
                          && stringCompare(routeDataResult.Values[ROUTE_VALUE_KEY_ACTION], expectedAction);

            if (expectedRouteValues != null) {
                PropertyInfo[] propertyInfos = expectedRouteValues.GetType().GetProperties();
                if (propertyInfos.Any(propertyInfo => !(routeDataResult.Values.ContainsKey(propertyInfo.Name)
                                                        && stringCompare(routeDataResult.Values[propertyInfo.Name], propertyInfo.GetValue(expectedRouteValues, null))))) {
                    result = false;
                }
            }

            return result;
        }

        #endregion

    }
}
