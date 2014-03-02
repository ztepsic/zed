using Zed.Core.Domain;

namespace Zed.Core.Tests.Domain.Entities {
    class Car : Entity {
        public string Name { get; set; }
        public float TopSpeed { get; set; }
    }
}
