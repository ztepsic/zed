﻿using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Zed.Web.Helpers;
using Zed.Web.Test;

namespace Zed.Web.Tests.Helpers {
    [TestFixture]
    public class HtmlHelperExtensionsTests {

        [Test]
        public void GetActiveCssClassIfRouteActive_ActiveRoute_ActiveCssClass() {
            // Arrange
            
            var mockedHtmlHelperBuilder = new MockedHtmlHelperBuilder();
            mockedHtmlHelperBuilder.ViewContextMock.Setup(m => m.RouteData)
               .Returns(new RouteData() {
                   Values = {
                        {"controller", "controllerName"},
                        {"action", "actionName"},
                        {"id", "111"}
                    }
               });

            HtmlHelper htmlHelper = mockedHtmlHelperBuilder.GetResult();

           
            // Act
            var cssActiveClass = htmlHelper.GetCssClassIfCurrentRouteIs("active", "controllerName", "actionName");
            var cssActiveClassWithRouteValues = htmlHelper.GetCssClassIfCurrentRouteIs("active", "controllerName", "actionName", new { id = 111 });

            // Assert
            Assert.AreEqual("active", cssActiveClass);
            Assert.AreEqual("active", cssActiveClassWithRouteValues);
        }


        [Test]
        public void GetActiveCssClassIfRouteActive_NotActiveRoute_ActiveCssClass() {
            // Arrange
            var mockedHtmlHelperBuilder = new MockedHtmlHelperBuilder();
            mockedHtmlHelperBuilder.ViewContextMock.Setup(m => m.RouteData)
               .Returns(new RouteData() {
                   Values = {
                        {"controller", "controllerName"},
                        {"action", "actionName"},
                        {"id", "111"}
                    }
               });

            HtmlHelper htmlHelper = mockedHtmlHelperBuilder.GetResult();


            // Act
            var cssActiveClass = htmlHelper.GetCssClassIfCurrentRouteIs("active", "controllerNameNotActive", "actionNameNotActive");
            var cssActiveClassWithRouteValues = htmlHelper.GetCssClassIfCurrentRouteIs("active", "controllerName", "actionName", new { id = 333 });

            // Assert
            Assert.IsEmpty(cssActiveClass);
            Assert.IsEmpty(cssActiveClassWithRouteValues);
        }

        [Test]
        public void GetActiveCssClassIfRouteActive_ActiveRouteMatchOnlyByController_ActiveCssClass() {
            // Arrange

            var mockedHtmlHelperBuilder = new MockedHtmlHelperBuilder();
            mockedHtmlHelperBuilder.ViewContextMock.Setup(m => m.RouteData)
               .Returns(new RouteData() {
                   Values = {
                        {"controller", "controllerName"},
                        {"action", "actionName"},
                        {"id", "111"}
                    }
               });

            HtmlHelper htmlHelper = mockedHtmlHelperBuilder.GetResult();


            // Act
            var cssActiveClass = htmlHelper.GetCssClassIfCurrentRouteIs("active", "controllerName");

            // Assert
            Assert.AreEqual("active", cssActiveClass);
        }

    }
}
