using System.IO;
using System.Web;
using System.Web.Mvc;
using Moq;
using System.Web.Routing;

namespace Zed.Web.Test {
    /// <summary>
    /// Class that builds mocked html helper.
    /// </summary>
    public class MockedHtmlHelperBuilder {

        #region Fields and Properties

        private readonly Mock<HttpContextBase> httpContextMock = new Mock<HttpContextBase>();

        /// <summary>
        /// Gets <see cref="HttpContext"/> mock
        /// </summary>
        public Mock<HttpContextBase> HttpContextMock { get { return httpContextMock; } }

        private readonly Mock<ControllerBase> controllerBaseMock = new Mock<ControllerBase>();

        /// <summary>
        /// Gets <see cref="ControllerBase"/> mock
        /// </summary>
        public Mock<ControllerBase> ControllerBaseMock { get { return controllerBaseMock; } }

        private readonly Mock<IView> viewMock = new Mock<IView>();

        /// <summary>
        /// Gets <see cref="IView"/> mock
        /// </summary>
        public Mock<IView> ViewMock { get { return viewMock; } }

        private readonly Mock<IViewDataContainer> viewDataContainerMock = new Mock<IViewDataContainer>();

        /// <summary>
        /// Gets <see cref="IViewDataContainer"/> mock
        /// </summary>
        public Mock<IViewDataContainer> ViewDataContainerMock { get { return viewDataContainerMock; } }

        private readonly Mock<ViewContext> viewContextMock;

        /// <summary>
        /// Gets <see cref="ViewContext"/> mock
        /// </summary>
        public Mock<ViewContext> ViewContextMock { get { return viewContextMock; } }

        private readonly ViewDataDictionary viewData;

        /// <summary>
        /// Gets view data
        /// </summary>
        public ViewDataDictionary ViewData { get { return viewData; } }

        private readonly RouteData routeData;

        /// <summary>
        /// Gets route data
        /// </summary>
        public RouteData RouteData { get { return routeData; } }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates an instance of html helper mock builder
        /// </summary>
        public MockedHtmlHelperBuilder() : this(new RouteData(), new ViewDataDictionary()) { }

        /// <summary>
        /// Creates an instance of html helper mock builder
        /// </summary>
        /// <param name="routeData">Route data</param>
        /// /// <param name="viewData">View data</param>
        public MockedHtmlHelperBuilder(RouteData routeData, ViewDataDictionary viewData) {
            this.routeData = routeData;
            this.viewData = viewData;

            ControllerContext controllerContext = new ControllerContext(httpContextMock.Object, routeData, controllerBaseMock.Object);

            viewContextMock = new Mock<ViewContext>(
                controllerContext,
                viewMock.Object,
                viewData,
                new TempDataDictionary(),
                new StreamWriter(new MemoryStream())
            );
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns building result - mocked html helper
        /// </summary>
        /// <returns>Mocked html helper</returns>
        public HtmlHelper GetResult() {
            return new HtmlHelper(viewContextMock.Object, viewDataContainerMock.Object);
        }

        /// <summary>
        /// Returns building result - mocked html helper for provieded type
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns>Mocked html helper</returns>
        public HtmlHelper<TModel> GetResult<TModel>() {
            return new HtmlHelper<TModel>(viewContextMock.Object, viewDataContainerMock.Object);
            
        }

        #endregion

    }
}
