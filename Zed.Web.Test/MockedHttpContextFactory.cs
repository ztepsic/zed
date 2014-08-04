using System.Web;
using Moq;

namespace Zed.Web.Test {
    /// <summary>
    /// Class that represents mocked http context factory.
    /// </summary>
    public static class MockedHttpContextFactory {

        #region Methods

        /// <summary>
        /// Creates http context based on <see cref="Mock"/>object.
        /// </summary>
        /// <param name="targetUrl">Target/request URL</param>
        /// <param name="httpMethod">Http method</param>
        /// <returns>Http context mocked http context</returns>
        public static HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET") {
            // create the mock request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(s => s);

            // create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        #endregion

    }
}
