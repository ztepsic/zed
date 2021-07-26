using NUnit.Framework;
using Zed.Domain;
using Zed.Tests.Domain.Entities;

namespace Zed.Tests.Domain {
    [TestFixture]
    public class EntityIdSetterTests {

        [Test]
        public void Can_Set_Id_To_Entity() {
            // Arrange
            Lion lion = new Lion();
            const int ID = 123;

            // Act
            lion.SetIdTo(ID);

            // Assert
            Assert.AreEqual(ID, lion.Id);

        }
    }
}
