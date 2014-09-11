using Zed.Domain;

namespace Zed.Tests.Domain.Entities {
    class Car : Entity {
        public string Name { get; set; }
        public float TopSpeed { get; set; }
    }
}
