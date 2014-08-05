using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Zed.Web.Models {
    /// <summary>
    /// Class that represents the current page's location within a navigational hierarchy.
    /// </summary>
    public class BreadcrumbsModel {

        /// <summary>
        /// Class that represent breadcrumb
        /// </summary>
        public class Breadcrumb {
            /// <summary>
            /// Breadcrum URL
            /// </summary>
            public string Url { get; internal set; }

            /// <summary>
            /// Credcreumb title
            /// </summary>
            public string Title { get; internal set; }

            /// <summary>
            /// Idicates is breadcumb active
            /// </summary>
            public bool IsActive { get; internal set; }
        }

        #region Fields and Properties

        private readonly IList<Breadcrumb> breadcrumbs = new List<Breadcrumb>();

        /// <summary>
        /// Gets breadcrumbs
        /// </summary>
        public IEnumerable<Breadcrumb> Breadcrumbs { get { return new ReadOnlyCollection<Breadcrumb>(breadcrumbs); } }

        #endregion

        #region Constructors and Init

        #endregion

        #region Methods

        /// <summary>
        /// Adds breadcrumb to breadcrumbs collection.
        /// When the active breadcrumb is added no more adding is allowed.
        /// </summary>
        /// <param name="title">Breadcrumb title</param>
        /// <param name="url">Breadcrumb url</param>
        /// <returns>Current instance</returns>
        public BreadcrumbsModel Add(string title, string url) {
            if (breadcrumbs.Any(b => b.IsActive)) { throw new InvalidOperationException("Active breadcrumb is added. No more breadcrumbs can be added."); }
            breadcrumbs.Add(new Breadcrumb { Title = title, Url = url, IsActive = false });
            return this;
        }

        /// <summary>
        /// Adds active breadcrum to breadcrumbs collection.
        /// When the active breadcrumb is added no more adding is allowed.
        /// </summary>
        /// <param name="title">Breadcrumb title</param>
        public void AddActive(string title) {
            if (breadcrumbs.Any(b => b.IsActive)) { throw new InvalidOperationException("Active breadcrumb is added. No more breadcrumbs can be added."); }
            breadcrumbs.Add(new Breadcrumb { Title = title, IsActive = true });
        }



        

        #endregion

    }
}
