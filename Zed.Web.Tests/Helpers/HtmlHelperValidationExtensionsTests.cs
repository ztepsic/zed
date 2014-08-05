using System;
using System.Web.Mvc;
using NUnit.Framework;
using Zed.Web.Extensions;
using Zed.Web.Test;
using Zed.Web.Helpers;

namespace Zed.Web.Tests.Helpers {
    [TestFixture]
    class HtmlHelperValidationExtensionsTests {

        public class TestModel {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Other { get; set; }
        }

        [Test]
        public void HasValidationErrorFor_ValidAndInvalidModelProperties_IndicatorTrueOrFalse() {
            // Arrange
            var mockedHtmlHelperBuilder = new MockedHtmlHelperBuilder();
            
            mockedHtmlHelperBuilder.ViewData.ModelState.AddModelError("Name", new Exception());
            mockedHtmlHelperBuilder.ViewDataContainerMock.Setup(m => m.ViewData)
                .Returns(mockedHtmlHelperBuilder.ViewData);
            mockedHtmlHelperBuilder.ViewContextMock.Setup(m => m.FormContext)
                .Returns(new FormContext());

            HtmlHelper<TestModel> htmlHelper = mockedHtmlHelperBuilder.GetResult<TestModel>();

            // Act
            var validationErrorResultName = htmlHelper.HasValidationErrorFor(m => m.Name);
            var validationErrorResultDescription = htmlHelper.HasValidationErrorFor(m => m.Description);

            // Assert
            Assert.IsTrue(validationErrorResultName);
            Assert.IsFalse(validationErrorResultDescription);
        }

        [Test]
        public void ValidationErrorCssClassFor_ValidAndInvalidModelProperties_ErrorCssClassNameInCaseOfErrorOtherwiseEmptyString() {
            // Arrange
            var mockedHtmlHelperBuilder = new MockedHtmlHelperBuilder();

            mockedHtmlHelperBuilder.ViewData.ModelState.AddModelError("Name", new Exception());
            mockedHtmlHelperBuilder.ViewDataContainerMock.Setup(m => m.ViewData)
                .Returns(mockedHtmlHelperBuilder.ViewData);
            mockedHtmlHelperBuilder.ViewContextMock.Setup(m => m.FormContext)
                .Returns(new FormContext());

            HtmlHelper<TestModel> htmlHelper = mockedHtmlHelperBuilder.GetResult<TestModel>();

            // Act
            var validationErrorResultName = htmlHelper.ValidationErrorCssClassFor(m => m.Name, "error");
            var validationErrorResultDescription = htmlHelper.ValidationErrorCssClassFor(m => m.Description, "error");

            // Assert
            Assert.AreEqual("error", validationErrorResultName);
            Assert.IsEmpty(validationErrorResultDescription);
        }

        [Test]
        public void ValidationCssClassFor_ModelPropertyAndValidationCssClassNames_ValidationCssClassNameDependingOfModelPropertyValidity() {
            // Arrange
            var mockedHtmlHelperBuilder = new MockedHtmlHelperBuilder();

            mockedHtmlHelperBuilder.ViewData.ModelState.AddModelError<TestModel>(m => m.Name, new Exception());
            mockedHtmlHelperBuilder.ViewData.ModelState.Add<TestModel>(m => m.Other, new ModelState());
            mockedHtmlHelperBuilder.ViewDataContainerMock.Setup(m => m.ViewData)
                .Returns(mockedHtmlHelperBuilder.ViewData);
            mockedHtmlHelperBuilder.ViewContextMock.Setup(m => m.FormContext)
                .Returns(new FormContext());

            HtmlHelper<TestModel> htmlHelper = mockedHtmlHelperBuilder.GetResult<TestModel>();

            // Act
            var validationErrorResultName = htmlHelper.ValidationCssClassFor(m => m.Name, "success", "error");
            var validationErrorResultDescription = htmlHelper.ValidationCssClassFor(m => m.Description, "success", "error");
            var validationErrorResultOther = htmlHelper.ValidationCssClassFor(m => m.Other, "success", "error");

            // Assert
            Assert.AreEqual("error", validationErrorResultName);
            Assert.IsEmpty(validationErrorResultDescription);
            Assert.AreEqual("success", validationErrorResultOther);
        }
    }
}
