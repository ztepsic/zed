using Zed.Domain;

namespace Zed.Tests.Domain.Entities {
    abstract class Animal : Entity {
        public int Age { get; set; }
        public string Food { get; set; }
        public string Gender { get; set; }
    }
}
