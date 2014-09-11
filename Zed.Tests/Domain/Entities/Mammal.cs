using System;

namespace Zed.Tests.Domain.Entities {
    abstract class Mammal : Animal {
        public string FurColor { get; set; }

        public void Sleep() {
            Console.WriteLine("Sleeping: Zzzzz");
        }

        public void MakeNoise() {
            Console.WriteLine("Noisess...");
        }

    }
}
