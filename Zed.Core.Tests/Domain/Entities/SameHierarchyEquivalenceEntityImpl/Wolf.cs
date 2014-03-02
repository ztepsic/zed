using System;

namespace Zed.Core.Tests.Domain.Entities.SameHierarchyEquivalenceEntityImpl {
    class Wolf : Mammal {
        public float NightVisionDistance { get; set; }

        public string LivesInGroup { get; set; }

        public int SizeOfGroup { get; set; }

        public void MarkTerritory(string territory) {
            Console.WriteLine("Territory " + territory + " marked.");
        }
    }
}
