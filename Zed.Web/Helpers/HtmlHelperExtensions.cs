using System.Web.Mvc;
using Zed.Web.Routes;

namespace Zed.Web.Helpers {
    /// <summary>
    /// HTML helper/extension methods
    /// </summary>
    public static class HtmlHelperExtensions {

        #region Methods

        /// <summary>
        /// Returns provided css class if provided route data represents current ative route, otherwise it returns empty string.
        /// </summary>
        /// <param name="html">Html helper.</param>
        /// <param name="cssClass">Css active class name.</param>
        /// <param name="controllerName">Controller name.</param>
        /// <param name="actionName">Action name.</param>
        /// <param name="routeValues">Other route values</param>
        /// <returns>Returns provided css class if provided route data represents current ative route, otherwise empty string.</returns>
        public static string GetCssClassIfCurrentRouteIs(this HtmlHelper html, string cssClass, string controllerName, string actionName = null, object routeValues = null) {
            return html.ViewContext.RouteData.MatchWith(actionName, controllerName, routeValues)
                ? cssClass
                : string.Empty;
        }

        #endregion

    }
}
