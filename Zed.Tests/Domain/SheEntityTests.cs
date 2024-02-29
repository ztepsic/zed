using Xunit;
using Zed.Domain;
using Zed.Tests.Domain.Entities.SameHierarchyEquivalenceEntityImpl;

namespace Zed.Tests.Domain {
    
    public class SheEntityTests {

        /// <summary>
        /// Three entities are not equal if they have the same identifier and different type in the same hierarchy.
        /// For any non-null reference values x, y, z, if x.Equals(y) returns true and
        /// y.Equals(z) return true, then x.Equals(z) must return true.
        /// </summary>
        [Fact]
        public void Three_Entities_Are__Not_Equal_If_They_Have_Same_Identifier_And_Different_Type_In_Same_Hierarchy_Equals_Implements_Transitive_Equivalence_Relation() {
            Wolf wolfX = new Wolf() {
                Age = 10,
                Food = "Meat",
                FurColor = "Grey",
                Gender = "Male",
                LivesInGroup = "Grey Wolves",
                SizeOfGroup = 10,
                NightVisionDistance = 20
            };

            wolfX.SetIdTo(1);

            Dog dogY = new Dog() {
                Age = 20,
                Food = "Pet food",
                FurColor = "Gold",
                Gender = "Male",
                HelpingPeopleAs = "Pet",
                Name = "Rex"
            };

            dogY.SetIdTo(1);


            Lion lionZ = new Lion() {
                Age = 30,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Brown",
                HasMane = true,
                SizeOfMane = 30,
            };

            lionZ.SetIdTo(1);

            // Act
            var wolfDogEntityEquivalenceResult = wolfX.Equals(dogY);
            var dogLionEntityEquivalenceResult = dogY.Equals(lionZ);
            var wolfLionEntityEquivalenceResult = wolfX.Equals(lionZ);
            var lionsHashCodeEquivalenceResultXY = wolfX.GetHashCode().Equals(dogY.GetHashCode());
            var lionsHashCodeEquivalenceResultYZ = dogY.GetHashCode().Equals(lionZ.GetHashCode());
            var lionsHashCodeEquivalenceResultXZ = wolfX.GetHashCode().Equals(lionZ.GetHashCode());

            // Assert
            Assert.NotNull(wolfX);
            Assert.NotNull(dogY);
            Assert.NotNull(lionZ);
            Assert.True(wolfDogEntityEquivalenceResult);
            Assert.True(dogLionEntityEquivalenceResult);
            Assert.True(wolfLionEntityEquivalenceResult);
            Assert.True(lionsHashCodeEquivalenceResultXY);
            Assert.True(lionsHashCodeEquivalenceResultYZ);
            Assert.True(lionsHashCodeEquivalenceResultXZ);
        }
    }
}
