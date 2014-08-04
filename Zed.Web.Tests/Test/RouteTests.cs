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
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            const string url1 = "~/";
            const string url2 = "~/testC/testA/111";

            // Assert
            Assert.IsTrue(RouteTest.RouteMatch(url1, routes, "Home", "Index"));
            Assert.IsTrue(RouteTest.RouteMatch(url2, routes, "testC", "testA", new { id = 111 }));

        }

    }
}
