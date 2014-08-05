using System.Web.Mvc;
using NUnit.Framework;
using Zed.Web.Helpers;
using Zed.Web.Test;
using System.Web.Routing;

namespace Zed.Web.Tests.Helpers {

    [TestFixture]
    public class UrlHelperExtensionsTests {

        [Test]
        public void IsRouteActive_ActiveRoute_True() {
            // Arrange
            const string expectedActionName = "actionName";
            const string expectedControllerName = "controllerName";
            var expectedRouteValues = new { id = 111 };

            var mockedHttpContextBuilder = new MockedHttpContextBuilder();

            var mockedUrlHelperBuilder = new MockedUrlHelperBuilder {
                RouteData = new RouteData() {
                    Values = {
                        {"controller", expectedControllerName},
                        {"action", expectedActionName},
                        {"id", "111"}
                    }
                },
                HttpContext = mockedHttpContextBuilder.GetResult()
            };

            UrlHelper urlHelper = mockedUrlHelperBuilder.GetResult();

            // Act
            var routeActive = urlHelper.IsRouteActive(expectedActionName, expectedControllerName, expectedRouteValues);
            var routeNotActive = urlHelper.IsRouteActive(expectedActionName+"NOT", expectedControllerName, expectedRouteValues);
            

            // Assert
            Assert.IsTrue(routeActive);
            Assert.IsFalse(routeNotActive);
        }

    }
}
