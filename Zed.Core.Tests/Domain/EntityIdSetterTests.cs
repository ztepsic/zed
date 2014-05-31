using NUnit.Framework;
using Zed.Core.Domain;
using Zed.Core.Tests.Domain.Entities;

namespace Zed.Core.Tests.Domain {
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
