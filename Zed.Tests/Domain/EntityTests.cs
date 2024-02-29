using Xunit;
using Zed.Domain;
using Zed.Tests.Domain.Entities;

namespace Zed.Tests.Domain {
    
    public class EntityTests {

        /// <summary>
        /// For any non-null reference value x, x.Equals(null) must return false
        /// </summary>
        [Fact]
        public void Equals_Returns_False_For_Provided_Null_Value() {
            // Arrange
            Lion lion = new Lion() {
                Age = 5,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 50,
            };

            lion.SetIdTo(1);

            // Act
            var entityEquivalenceResult = lion.Equals(null);

            // Assert
            Assert.NotNull(lion);
            Assert.False(entityEquivalenceResult);
        }

        /// <summary>
        /// For any non-null reference value x, x.Equals(x) must return true.
        /// </summary>
        [Fact]
        public void With_Identifier_Set_Equals_Implements_Reflexive_Equivalence_Relation() {
            // Arrange
            Lion lion = new Lion() {
                Age = 5,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 50,
            };

            lion.SetIdTo(1);

            // Act
            var entityEquivalenceResult = lion.Equals(lion);
            var hashCodeEquivalenceResult = lion.GetHashCode().Equals(lion.GetHashCode());

            // Assert
            Assert.NotNull(lion);
            Assert.True(entityEquivalenceResult);
            Assert.True(hashCodeEquivalenceResult);
        }

        /// <summary>
        /// For any non-null reference value x, x.Equals(x) must return true.
        /// </summary>
        [Fact]
        public void Without_Identifier_Set_Equals_Implements_Reflexive_Equivalence_Relation() {
            // Arrange
            Lion lion = new Lion() {
                Age = 5,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 50,
            };

            // Act
            var entityEquivalenceResult = lion.Equals(lion);
            var hashCodeEquivalenceResult = lion.GetHashCode().Equals(lion.GetHashCode());

            // Assert
            Assert.NotNull(lion);
            Assert.True(entityEquivalenceResult);
            Assert.True(hashCodeEquivalenceResult);
        }

        /// <summary>
        /// Two entities are equal if they have the same identifier and same type.
        /// For any non-null reference values x and y, x.Equals(y) must return true if and only if y.Equals(x) returns true.
        /// </summary>
        [Fact]
        public void Two_Entities_Are_Equal_If_They_Have_Same_Identifier_And_Type_Equals_Implements_Symmetric_Equivalence_Relation() {
            Lion lionX = new Lion() {
                Age = 10,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 10,
            };

            lionX.SetIdTo(1);

            Lion lionY = new Lion() {
                Age = 20,
                Food = "Gazzelle",
                Gender = "Female",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 20,
            };

            lionY.SetIdTo(1);

            // Act
            var entitiesEquivalenceResultXY = lionX.Equals(lionY);
            var entitiesEquivalenceResultYX = lionY.Equals(lionX);
            var hashCodeEquivalenceResult = lionX.GetHashCode().Equals(lionY.GetHashCode());

            // Assert
            Assert.NotNull(lionX);
            Assert.NotNull(lionY);
            Assert.True(entitiesEquivalenceResultXY);
            Assert.True(entitiesEquivalenceResultYX);
            Assert.True(hashCodeEquivalenceResult);
        }

        /// <summary>
        /// Two entities of the same type are not equal if they don't have the same identifier.
        /// For any non-null reference values x and y, x.Equals(y) must return true if and only if y.Equals(x) returns true.
        /// </summary>
        [Fact]
        public void Two_Entites_Of_Same_Type_Are_Not_Equal_If_They_Dont_Have_Same_Identifier_Equals_Implements_Symmetric_Equivalence_Relation() {
            Lion lionX = new Lion() {
                Age = 10,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 10,
            };

            lionX.SetIdTo(1);

            Lion lionY = new Lion() {
                Age = 20,
                Food = "Gazzelle",
                Gender = "Female",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 20,
            };

            lionY.SetIdTo(2);

            // Act
            var entitiesEquivalenceResultXY = lionX.Equals(lionY);
            var entitiesEquivalenceResultYX = lionY.Equals(lionX);

            // Assert
            Assert.NotNull(lionX);
            Assert.NotNull(lionY);
            Assert.False(entitiesEquivalenceResultXY);
            Assert.False(entitiesEquivalenceResultYX);
        }

        /// <summary>
        /// Two entities of different type hiearachy with the same identifier are not equal.
        /// For any non-null reference values x and y, x.Equals(y) must return true if and only if y.Equals(x) returns true.
        /// </summary>
        [Fact]
        public void Two_Entities_Of_Different_Type_Hierarchy_With_Same_Identifier_Are_Not_Equal_Equals_Implements_Symmetric_Equivalence_Relation() {
            Lion lionX = new Lion() {
                Age = 10,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 10,
            };

            lionX.SetIdTo(1);

            Car carY = new Car() {
                Name = "Audi",
                TopSpeed = 250
            };

            carY.SetIdTo(1);

            // Act
            var entitiesEquivalenceResultXY = lionX.Equals(carY);
            var entitiesEquivalenceResultYX = carY.Equals(lionX);

            // Assert
            Assert.NotNull(lionX);
            Assert.NotNull(carY);
            Assert.False(entitiesEquivalenceResultXY);
            Assert.False(entitiesEquivalenceResultYX);
        }

        /// <summary>
        /// Three entities are equal if they have the same identifier and type.
        /// For any non-null reference values x, y, z, if x.Equals(y) returns true and
        /// y.Equals(z) return true, then x.Equals(z) must return true.
        /// </summary>
        [Fact]
        public void Three_Entities_Are_Equal_If_They_Have_Same_Identifier_And_Type_Equals_Implements_Transitive_Equivalence_Relation() {
            Lion lionX = new Lion() {
                Age = 10,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 10,
            };

            lionX.SetIdTo(1);

            Lion lionY = new Lion() {
                Age = 20,
                Food = "Gazzelle",
                Gender = "Feale",
                FurColor = "Yellow",
                HasMane = false,
                SizeOfMane = 20,
            };

            lionY.SetIdTo(1);

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
            var entitiesEquivalenceResultXY = lionX.Equals(lionY);
            var entitiesEquivalenceResultYZ = lionY.Equals(lionZ);
            var entitiesEquivalenceResultXZ = lionX.Equals(lionZ);
            var hashCodeEquivalenceResultXY = lionX.GetHashCode().Equals(lionY.GetHashCode());
            var hashCodeEquivalenceResultYZ = lionY.GetHashCode().Equals(lionZ.GetHashCode());
            var hashCodeEquivalenceResultXZ = lionX.GetHashCode().Equals(lionZ.GetHashCode());

            // Assert
            Assert.NotNull(lionX);
            Assert.NotNull(lionY);
            Assert.NotNull(lionZ);
            Assert.True(entitiesEquivalenceResultXY);
            Assert.True(entitiesEquivalenceResultYZ);
            Assert.True(entitiesEquivalenceResultXZ);
            Assert.True(hashCodeEquivalenceResultXY);
            Assert.True(hashCodeEquivalenceResultYZ);
            Assert.True(hashCodeEquivalenceResultXZ);
        }

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
            Assert.False(wolfDogEntityEquivalenceResult);
            Assert.False(dogLionEntityEquivalenceResult);
            Assert.False(wolfLionEntityEquivalenceResult);
            Assert.False(lionsHashCodeEquivalenceResultXY);
            Assert.False(lionsHashCodeEquivalenceResultYZ);
            Assert.False(lionsHashCodeEquivalenceResultXZ);
        }

        [Fact]
        public void Transitive_And_Persistent_Entities_With_Same_Data_Are_Not_Equal() {
            // Arrange
            Lion lionPersistent = new Lion() {
                Age = 5,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 50,
            };

            lionPersistent.SetIdTo(1);

            Lion lionTransitive = new Lion() {
                Age = 5,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 50,
            };

            // Act - Symetric Equivalence
            var equivalenceResultPT = lionPersistent.Equals(lionTransitive);
            var equivalenceResultTP = lionTransitive.Equals(lionPersistent);

            // Assert
            Assert.False(equivalenceResultPT);
            Assert.False(equivalenceResultTP);

        }

        [Fact]
        public void Two_Transitive_Entities_Are_Equal_Only_If_Thay_Have_Same_Reference() {
            // Arrange
            Lion lionA = new Lion() {
                Age = 5,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 50,
            };

            Lion lionB = new Lion() {
                Age = 5,
                Food = "Gazzelle",
                Gender = "Male",
                FurColor = "Yellow",
                HasMane = true,
                SizeOfMane = 50,
            };

            Lion lionC = lionA;

            // Act
            var areEqualLionALionB = lionA.Equals(lionB);
            var hashCodeEquivalenceResultLionALionB = lionA.GetHashCode().Equals(lionB.GetHashCode());
            var areEqualLionALionC = lionA.Equals(lionC);
            var hashCodeEquivalenceResultLionALionC = lionA.GetHashCode().Equals(lionC.GetHashCode());

            // Assert
            Assert.False(areEqualLionALionB);
            Assert.False(hashCodeEquivalenceResultLionALionB);
            Assert.True(areEqualLionALionC);
            Assert.True(hashCodeEquivalenceResultLionALionC);
        }
    }
}
