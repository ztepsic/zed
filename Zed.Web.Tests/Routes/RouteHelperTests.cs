using System.Web.Routing;
using NUnit.Framework;
using Zed.Web.Routes;

namespace Zed.Web.Tests.Routes {
    [TestFixture]
    public class RouteHelperTests {

        [Test]
        public void RouteDataMatchWith_RouteValuesThatMatch_RouteMatch() {
            // Arrange
            const string expectedActionName = "actionName";
            const string expectedControllerName = "controllerName";
            var expectedRouteValues = new { id = 111 };

            RouteData routeData = new RouteData() {
                Values = {
                    { "controller",  expectedControllerName },
                    { "action", expectedActionName },
                    { "id", "111" }
                }
            };

            // Act
            var isMatched = RouteHelper.RouteDataMatchWith(routeData, expectedControllerName, expectedActionName, expectedRouteValues);

            // Assert
            Assert.IsTrue(isMatched);

        }

        [Test]
        public void RouteDataMatchWith_RouteValuesThatNotMatch_RouteNotMatch() {
            // Arrange
            const string expectedActionName = "actionName";
            const string expectedControllerName = "controllerName";
            var expectedRouteValues = new { id = 111 };

            RouteData routeData = new RouteData() {
                Values = {
                    { "controller",  expectedControllerName+"NOT" },
                    { "action", expectedActionName+"NOT" },
                    { "id", "111" }
                }
            };

            // Act
            var isMatched = RouteHelper.RouteDataMatchWith(routeData, expectedControllerName, expectedActionName, expectedRouteValues);

            // Assert
            Assert.IsFalse(isMatched);

        }

        [Test]
        public void RouteDataMatchWith_RouteValuesThatMatchBasedOnController_RouteMatch() {
            // Arrange
            const string expectedActionName = "actionName";
            const string expectedControllerName = "controllerName";
            var expectedRouteValues = new { id = 111 };

            RouteData routeData = new RouteData() {
                Values = {
                    { "controller",  expectedControllerName },
                    { "action", expectedActionName },
                    { "id", "111" }
                }
            };

            // Act
            var isMatched = RouteHelper.RouteDataMatchWith(routeData, expectedControllerName, expectedActionName, expectedRouteValues);

            // Assert
            Assert.IsTrue(isMatched);

        }

    }
}
