using System.Web.Mvc;
using Zed.Web.Routes;

namespace Zed.Web.Helpers {
    /// <summary>
    /// URL helper/extension methods
    /// </summary>
    public static class UrlHelperExtensions {

        #region Methods

        /// <summary>
        /// Checks if provided route is active.
        /// </summary>
        /// <param name="url">Url helper.</param>
        /// <param name="actionName">Action name.</param>
        /// <param name="controllerName">Controller name.</param>
        /// <param name="routeValues">Other route values</param>
        /// <returns>True if provieded route is active, otherwise false.</returns>
        public static bool IsRouteActive(this UrlHelper url, string actionName, string controllerName, object routeValues = null) {
            return url.RequestContext.RouteData.MatchWith(actionName, controllerName, routeValues);
        }

        #endregion

    }
}
