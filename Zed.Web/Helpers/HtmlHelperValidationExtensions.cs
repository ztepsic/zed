using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Zed.Web.Extensions;

namespace Zed.Web.Helpers {
    /// <summary>
    /// HTML validation helper/extension methods
    /// </summary>
    public static class HtmlHelperValidationExtensions {

        /// <summary>
        /// Checks the ModelState for an error, and returns true if error exists, otherwise false
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The property of the model which we are cheking.</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">The expression tree representing a property to check if it is a valid field for the current model.</param>
        /// <returns>True if error exists, otherwise false.</returns>
        public static bool HasValidationErrorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) {
            FormContext formContext = htmlHelper.ViewContext.FormContext;
            if (formContext == null) return false;
            return !htmlHelper.ViewData.ModelState.IsValidField(expression);
        }

        /// <summary>
        /// Returns error css class name in case of invalid property, otherwise empty string
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The property of the model which we are cheking.</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">The expression tree representing a property to validate.</param>
        /// <param name="errorCssClassName">Error css class name.</param>
        /// <returns>Provided error css class name in case of invalid property, otherwise empty string.</returns>
        public static string ValidationErrorCssClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string errorCssClassName) {
            FormContext formContext = htmlHelper.ViewContext.FormContext;
            if (formContext == null) return string.Empty;
            return !htmlHelper.ViewData.ModelState.IsValidField(expression) ? errorCssClassName : string.Empty;
        }

        /// <summary>
        /// Returns provided class names depending if property is valid or invalid, or empty string
        /// if property has not been validated.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The property of the model which we are cheking.</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">The expression tree representing a property to validate.</param>
        /// <param name="successCssClassName">Success class name</param>
        /// <param name="errorCssClassName">Error class name</param>
        /// <returns>Sucess class name if property is valid, error class name if property is invalid,
        /// or empty string if property has not been validated.
        /// </returns>
        public static string ValidationCssClassFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string successCssClassName, string errorCssClassName) {
            FormContext formContext = htmlHelper.ViewContext.FormContext;
            if (formContext == null) return String.Empty;

            string propertyKey = ExpressionHelper.GetExpressionText(expression);
            string validationCssClass = String.Empty;
            if (!htmlHelper.ViewData.ModelState.ContainsKey(propertyKey)) return validationCssClass;

            var modelState = htmlHelper.ViewData.ModelState[propertyKey];
            if (modelState.Errors != null && modelState.Errors.Count > 0) {
                validationCssClass = errorCssClassName;
            } else {
                validationCssClass = successCssClassName;    
            }

            return validationCssClass;
        }

    }
}
