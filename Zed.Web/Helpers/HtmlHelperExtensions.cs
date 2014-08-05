using System.Web.Mvc;
using Zed.Web.Routes;

namespace Zed.Web.Helpers {
    /// <summary>
    /// HTML helper/extension methods
    /// </summary>
    public static class HtmlHelperExtensions {

        #region Methods

        /// <summary>
        /// Returns provided css class if provided route is active, otherwise it returns empty string.
        /// </summary>
        /// <param name="html">Html helper.</param>
        /// <param name="cssActiveClass">Css active class name.</param>
        /// <param name="actionName">Action name.</param>
        /// <param name="controllerName">Controller name.</param>
        /// <param name="routeValues">Other route values</param>
        /// <returns>If provided route is active returns provided css class, otherwise empty string.</returns>
        public static string GetActiveCssClassIfRouteActive(this HtmlHelper html, string cssActiveClass, string actionName, string controllerName, object routeValues = null) {
            return html.ViewContext.RouteData.MatchWith(actionName, controllerName, routeValues)
                ? cssActiveClass
                : string.Empty;
        }

        #endregion

    }
}
