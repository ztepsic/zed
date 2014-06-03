using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Zed.Web.Extensions {
    /// <summary>
    /// <see cref="System.Web.Mvc.ModelStateDictionary"/> extension methods
    /// </summary>
    public static class ModelStateDictionaryExtensions {

        /// <summary>
        /// Indicates if the expression is a valid field for the current model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The property of the model which we are cheking.</typeparam>
        /// <param name="modelStateDictionary">The model state dictionary.</param>
        /// <param name="expression">The expression tree representing a property to validate.</param>
        /// <returns>true if the expression is a valid field for the current model, otherwise false.</returns>
        public static bool IsValidField<TModel, TProperty>(this ModelStateDictionary modelStateDictionary, Expression<Func<TModel, TProperty>> expression) {
            if (expression == null) throw new ArgumentNullException("expression");
            return modelStateDictionary.IsValidField(ExpressionHelper.GetExpressionText(expression));
        }

        /// <summary>
        /// Add a model error.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="modelStateDictionary">The model state dictionary.</param>
        /// <param name="expression">The expression tree representing a property to add an error in its state.</param>
        /// <param name="errorMessage">The error message to add.</param>
        public static void AddModelError<TModel>(this ModelStateDictionary modelStateDictionary, Expression<Func<TModel, object>> expression, String errorMessage) {
            if (expression == null) throw new ArgumentNullException("expression");
            modelStateDictionary.AddModelError(ExpressionHelper.GetExpressionText(expression), errorMessage);
        }

        /// <summary>
        /// Add a model error.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="modelStateDictionary">The model state dictionary.</param>
        /// <param name="expression">The expression tree representing a property to add an error in its state.</param>
        /// <param name="exception">The exception to add as an error message container.</param>
        public static void AddModelError<TModel>(this ModelStateDictionary modelStateDictionary, Expression<Func<TModel, object>> expression, Exception exception) {
            if (expression == null) throw new ArgumentNullException("expression");
            modelStateDictionary.AddModelError(ExpressionHelper.GetExpressionText(expression), exception);
        }

        /// <summary>
        /// Add an element that has the specified key and the value to the model-state dictionary
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="modelStateDictionary">The model state dictionary.</param>
        /// <param name="expression">The expression tree representing a property to add an error in its state.</param>
        /// <param name="value">The value of the element to add.</param>
        public static void Add<TModel>(this ModelStateDictionary modelStateDictionary, Expression<Func<TModel, object>> expression, ModelState value) {
            if (expression == null) throw new ArgumentNullException("expression");
            modelStateDictionary.Add(ExpressionHelper.GetExpressionText(expression), value);
        }
    }
}
