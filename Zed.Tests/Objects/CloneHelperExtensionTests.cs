using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Zed.Objects;

namespace Zed.Tests.Objects {
    [TestFixture]
    public class CloneHelperExtensionTests {

        [Serializable]
        private class MyClass {
            public string Name { get; set; }
            public int Number { get; set; }
        }

        [Test]
        public void DeepClone_ObjectThatsNeedsToBeCloned_CreatesClonedObject() {
            // Arrange
            var objectA = new MyClass() {
                Name = "Object22",
                Number = 22
            };

            // Act
            var objectB = objectA.DeepClone();

            // Asert
            Assert.AreNotSame(objectA, objectB);

        }

        [Test]
        public void ToArrayWithDeepClone_ObjectArray_CreatesClonedArrayWithDeepClonedElements() {
            // Arrange
            IList<MyClass> myClasses = new List<MyClass> {
				new MyClass() {
					Name = "Object1",
					Number = 1
				},
				new MyClass() {
					Name = "Object2",
					Number = 2
				},
				new MyClass() {
					Name = "Object3",
					Number = 3
				}
			};

            var myClassesArray = myClasses.ToArray();

            // Act
            var myClassesArrayDeepCopy = myClasses.ToArrayWithDeepClone().ToArray();

            // Asert
            Assert.AreNotSame(myClassesArray, myClassesArrayDeepCopy);

            for (int i = 0; i < myClassesArrayDeepCopy.Count(); i++) {
                Assert.AreNotSame(myClassesArrayDeepCopy[i], myClassesArray[i]);
            }
        }

    }
}
