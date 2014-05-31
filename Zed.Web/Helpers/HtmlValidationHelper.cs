using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Zed.Web.Extensions;

namespace Zed.Web.Helpers {
    /// <summary>
    /// HTML validation helper/extension methods
    /// </summary>
    public static class HtmlValidationHelper {

        /// <summary>
        /// Checks the ModelState for an error, and returns true if error exists, otherwise false
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">The expression tree representing a property to check if it is a valid field for the current model.</param>
        /// <returns>True if error exists, otherwise false.</returns>
        public static bool HasValidationErrorFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> expression) {
            FormContext formContext = htmlHelper.ViewContext.FormContext;
            if (formContext == null) return false;
            return !htmlHelper.ViewData.ModelState.IsValidField(expression);
        }
    }
}
