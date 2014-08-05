using System;
using System.Linq;
using NUnit.Framework;
using Zed.Web.Models;

namespace Zed.Web.Tests.Models {
    [TestFixture]
    public class BreadcrumbsModelTests {

        [Test]
        public void Add_Breadcrumbs_BreadcrumbsInCollection() {
            // Arrange
            var breadcrumbsModel = new BreadcrumbsModel();

            // Act
            breadcrumbsModel.Add("Home", "/")
                .Add("Categories", "categories")
                .AddActive("Sports");

            // Assert
            var breadcrumbsArray = breadcrumbsModel.Breadcrumbs.ToArray();

            Assert.AreEqual("Home", breadcrumbsArray[0].Title);
            Assert.AreEqual("/", breadcrumbsArray[0].Url);
            Assert.IsFalse(breadcrumbsArray[0].IsActive);

            Assert.AreEqual("Categories", breadcrumbsArray[1].Title);
            Assert.AreEqual("categories", breadcrumbsArray[1].Url);
            Assert.IsFalse(breadcrumbsArray[1].IsActive);

            Assert.AreEqual("Sports", breadcrumbsArray[2].Title);
            Assert.IsNullOrEmpty(breadcrumbsArray[2].Url);
            Assert.IsTrue(breadcrumbsArray[2].IsActive);
        }

        
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_TwoAfterAddedActiveBreadcrumb_InvalidOperationExceptionThrown() {
            // Arrange
            var breadcrumbsModel = new BreadcrumbsModel();

            // Act
            breadcrumbsModel.Add("Home", "/")
                .Add("Categories", "categories")
                .AddActive("Sports");

            breadcrumbsModel.Add("News", "news");

            // Assert
            
        }

    }
}
