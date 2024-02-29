using Xunit;
using Zed.Domain;
using Zed.Tests.Domain.Entities;

namespace Zed.Tests.Domain {
    public class EntityIdSetterTests {

        [Fact]
        public void Can_Set_Id_To_Entity() {
            // Arrange
            Lion lion = new();
            const int ID = 123;

            // Act
            lion.SetIdTo(ID);

            // Assert
            Assert.Equal(ID, lion.Id);

        }
    }
}
