using System.Web;
using Moq;

namespace Zed.Web.Test {
    /// <summary>
    /// Class that builds mocked http context.
    /// </summary>
    public class MockedHttpContextBuilder {

        #region Fields and Properties

        private readonly Mock<HttpRequestBase> requestMock = new Mock<HttpRequestBase>();

        /// <summary>
        /// Gets request mock
        /// </summary>
        public Mock<HttpRequestBase> RequestMock { get { return requestMock; } }

        
        private readonly Mock<HttpResponseBase> responseMock = new Mock<HttpResponseBase>();

        /// <summary>
        /// Gets response mock
        /// </summary>
        public Mock<HttpResponseBase> ResponseMock { get { return responseMock; } }

        private readonly Mock<HttpContextBase> httpContextMock;

        /// <summary>
        /// Gets http context mock
        /// </summary>
        public Mock<HttpContextBase> HttpContextMock { get { return httpContextMock; } }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Created mocked http context builder
        /// </summary>
        public MockedHttpContextBuilder() {
            httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(m => m.Request).Returns(requestMock.Object);
            httpContextMock.Setup(m => m.Response).Returns(responseMock.Object);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns building result - mocked http context
        /// </summary>
        /// <returns>Mocked url helper</returns>
        public HttpContextBase GetResult() { return httpContextMock.Object; }

        #endregion


    }
}
