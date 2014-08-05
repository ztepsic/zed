using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace Zed.Web.Models {
    /// <summary>
    /// 
    /// </summary>
    public class PageInfoModel {

        #region Constants

        private const char KEYWORDS_SEPARATOR = ',';

        #endregion

        #region Fields and Properties

        /// <summary>
        /// Page title
        /// </summary>
        private string title;

        /// <summary>
        /// Gets page title
        /// </summary>
        public string Title { get { return title; } }

        /// <summary>
        /// Gets or Sets page description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Page keywords
        /// </summary>
        private ISet<string> keywordsSet;

        #endregion

        #region Constructros and Init

        /// <summary>
        /// Creates an instance of page info model
        /// </summary>
        /// <param name="title"></param>
        public PageInfoModel(string title) {
            this.title = title;

            keywordsSet = new HashSet<string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds keyword to keywords collection
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>Current isntance</returns>
        public PageInfoModel AddKeyword(string keyword) {
            keywordsSet.Add(keyword);
            return this;
        }

        /// <summary>
        /// Adds comma separated keywords to keywords collection
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public PageInfoModel AddKeywords(string keywords) {
            var keywordsArray = keywords.Split(KEYWORDS_SEPARATOR);

            foreach (var keyword in keywordsArray) {
                keywordsSet.Add(keyword.Trim());
            }

            return this;
        }

        /// <summary>
        /// Removes keyword from keywords collection
        /// </summary>
        /// <param name="keyword"></param>
        public void RemoveKeyword(string keyword) { keywordsSet.Remove(keyword); }

        /// <summary>
        /// Clears all keywords
        /// </summary>
        public void ClearKeywords() { keywordsSet.Clear(); }

        /// <summary>
        /// Gets all keywords separated by comma
        /// </summary>
        /// <returns>Keywords separated by comma</returns>
        public MvcHtmlString GetKeywords() { return new MvcHtmlString(string.Join(", ", keywordsSet)); }

        #endregion

    }
}
