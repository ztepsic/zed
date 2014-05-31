using Zed.Core.Domain;

namespace Zed.Core.Tests.Domain.Entities.DomainObjectsSignatureImpl {
    class Car : DosEntity {
        [BusinessKey]
        public string Name { get; set; }
        public float TopSpeed { get; set; }
    }
}
