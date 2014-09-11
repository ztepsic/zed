using System;

namespace Zed.Tests.Domain.Entities.SameHierarchyEquivalenceEntityImpl {
    abstract class Mammal : Animal {
        public string FurColor { get; set; }

        public void Sleep() {
            Console.WriteLine("Sleeping: Zzzzz");
        }

        public void MakeNoise() {
            Console.WriteLine("Noisess...");
        }

        /// <summary>
        /// By overriding this method and returning this type we have enabled equality by the same hierarchy
        /// and identifier, where this class is the root of hierarchy.
        /// </summary>
        /// <returns></returns>
        public override Type GetDomainObjectSignatureType() {
            return typeof(Mammal);
        }

    }
}
