using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Zed.Web.Test;

namespace Zed.Web.Tests.Test {
    [TestFixture]
    public class RouteTests {

        [Test]
        public void RouteMatch() {
            // Arrange
            RouteCollection routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);

            routes.MapRoute(
               name: "TestExample",
               url: "test/{action}/{id}",
               defaults: new { controller = "TestController", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            const string url1 = "~/";
            const string url2 = "~/testController/testAction/111";
            const string url3 = "~/test/";

            // Assert
            Assert.IsTrue(RouteTest.RouteMatch(url1, routes, "Index", "Home"));
            Assert.IsTrue(RouteTest.RouteMatch(url2, routes, "testAction", "testController", new { id = 111 }));
            Assert.IsTrue(RouteTest.RouteMatch(url3, routes, "Index", "testController"));

        }

        [Test]
        public void RouteNotMatch() {
            // Arrange
            RouteCollection routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            const string url1 = "~/testController/testAction/111/test-param";

            // Assert
            Assert.IsTrue(RouteTest.RouteNotMatch(url1, routes));
            
        }

    }
}
