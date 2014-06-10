using System;
using System.Web.Mvc;
using NUnit.Framework;
using Zed.Web.Extensions;

namespace Zed.Web.Tests.Extensions {

    public class Model {
        public string PropertyA { get; set; }
        public string PropertyB { get; set; }
    }

    [TestFixture]
    public class ModelStateDictionaryExtensionsTests {

        [Test]
        public void IsValidField_ValidAndInvalidModelFields_IndicatorTrueOrFalse() {
            // Arrange
            var model = new Model {PropertyA = "PropertyAValid", PropertyB = "PropertyBInvalid"};
            ModelStateDictionary modlModelStateDictionary = new ModelStateDictionary();
            modlModelStateDictionary.Add<Model>(x => x.PropertyA, new ModelState());
            modlModelStateDictionary.AddModelError<Model>(x => x.PropertyB, new Exception());

            // Act
            var isPropertyAValid = modlModelStateDictionary.IsValidField<Model, object>(x => x.PropertyA);
            var isPropertyBValid = modlModelStateDictionary.IsValidField<Model, object>(x => x.PropertyB);

            // Assert
            Assert.IsTrue(isPropertyAValid);
            Assert.IsFalse(isPropertyBValid);
        }

        [Test]
        public void AddModelError_InvalidModelField_ContainsAddedModelError() {
            // Arrange
            var model = new Model { PropertyA = "PropertyAValid", PropertyB = "PropertyBInvalid" };
            ModelStateDictionary modlModelStateDictionary = new ModelStateDictionary();
            modlModelStateDictionary.AddModelError<Model>(x => x.PropertyB, new Exception());

            // Act
            var containsModelError = modlModelStateDictionary.ContainsKey("PropertyB");

            // Assert
            Assert.IsTrue(containsModelError);
        }

        [Test]
        public void Add_Element_ContainsAddedElement() {
            // Arrange
            var model = new Model { PropertyA = "PropertyAValid", PropertyB = "PropertyBInvalid" };
            ModelStateDictionary modlModelStateDictionary = new ModelStateDictionary();
            modlModelStateDictionary.Add<Model>(x => x.PropertyA, new ModelState());

            // Act
            var containsKey = modlModelStateDictionary.ContainsKey("PropertyA");

            // Assert
            Assert.IsTrue(containsKey);
        }
    }
}
