using NUnit.Framework;
using Zed.Web.Models;

namespace Zed.Web.Tests.Models {
    [TestFixture]
    public class PageInfoModelTests {

        [Test]
        public void Ctor_PageInfoData_PageInfoModelInstance() {
            // Arrange
            const string pageTitle = "PageTitle";
            const string pageDescription = "PageDescription";

            // Act
            var pageInfoModel = new PageInfoModel(pageTitle) {
                Description = pageDescription
            };

            // Assert
            Assert.IsNotNull(pageInfoModel);
            Assert.AreEqual(pageTitle, pageInfoModel.Title);
            Assert.AreEqual(pageDescription, pageInfoModel.Description);
        }

        [Test]
        public void GetKeywords_Keywords_CommaSeparatedKeywords() {
            // Arrange
            const string keyword01 = "keyword01";
            const string keyword02 = "keyword02";
            const string keyword03 = "keyword03";
            var pageInfoModel = new PageInfoModel("PageTitle");

            pageInfoModel.AddKeyword(keyword01)
                .AddKeyword(keyword02)
                .AddKeyword(keyword03);

            // Act
            var keywords = pageInfoModel.GetKeywords().ToHtmlString();

            // Assert
            Assert.IsNotNullOrEmpty(keywords);
            Assert.IsTrue(keywords.Contains(keyword01));
            Assert.IsTrue(keywords.Contains(keyword02));
            Assert.IsTrue(keywords.Contains(keyword03));
            Assert.IsTrue(keywords.Contains(","));
        }

        [Test]
        public void AddKeywords() {
            // Arrange
            const string keywords = "keyword01, keyword02, keyword03";
            var pageInfoModel = new PageInfoModel("PageTitle");
            pageInfoModel.AddKeywords(keywords);

            // Act
            var keywordsResult = pageInfoModel.GetKeywords().ToHtmlString();

            // Assert
            Assert.IsNotNullOrEmpty(keywordsResult);
            Assert.AreEqual(keywords, keywordsResult);
        }

    }
}
