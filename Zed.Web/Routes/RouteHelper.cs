using System;
using System.Linq;
using System.Reflection;
using System.Web.Routing;

namespace Zed.Web.Routes {
    /// <summary>
    /// Collection of route helper methods
    /// </summary>
    public static class RouteHelper {

        #region Constants

        private const string ROUTE_VALUE_KEY_CONTROLLER = "controller";
        private const string ROUTE_VALUE_KEY_ACTION = "action";

        #endregion


        #region Methods

        /// <summary>
        /// Method which compares the route data with the expected segment variable values.
        /// </summary>
        /// <param name="routeData">Route data.</param>
        /// <param name="expectedControllerName">Expected controller name.</param>
        /// <param name="expectedActionName">Expected action name.</param>
        /// <param name="expectedRouteValues">Expected other segment variable values.</param>
        public static bool RouteDataMatchWith(RouteData routeData, string expectedControllerName, string expectedActionName = null, object expectedRouteValues = null) {
            // helper function to compare two strings
            Func<object, object, bool> stringCompare = (v1, v2) => string.Equals(v1.ToString(), v2.ToString(), StringComparison.InvariantCultureIgnoreCase);
            //Func<object, object, bool> stringCompare = (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;

            // compare controller and action values
            bool result = stringCompare(routeData.Values[ROUTE_VALUE_KEY_CONTROLLER], expectedControllerName);
            if (!string.IsNullOrEmpty(expectedActionName)) {
                result &= stringCompare(routeData.Values[ROUTE_VALUE_KEY_ACTION], expectedActionName);
            }
                          

            if (expectedRouteValues != null) {
                PropertyInfo[] propertyInfos = expectedRouteValues.GetType().GetProperties();
                if (propertyInfos.Any(propertyInfo => !(routeData.Values.ContainsKey(propertyInfo.Name)
                                                        && stringCompare(routeData.Values[propertyInfo.Name], propertyInfo.GetValue(expectedRouteValues, null))))) {
                    result = false;
                }
            }

            return result;
        }


        /// <summary>
        /// Method which compares the route data with the expected segment variable values.
        /// </summary>
        /// <param name="routeData">Route data.</param>
        /// <param name="expectedControllerName">Expected controller name.</param>
        /// <param name="expectedActionName">Expected action name.</param>
        /// <param name="expectedRouteValues">Expected other segment variable values.</param>
        public static bool MatchWith(this RouteData routeData, string expectedControllerName, string expectedActionName = null, object expectedRouteValues = null) {
            return RouteDataMatchWith(routeData, expectedActionName, expectedControllerName, expectedRouteValues);
        }

        #endregion

    }
}
