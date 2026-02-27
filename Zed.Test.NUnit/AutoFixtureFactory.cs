using AutoFixture;
using AutoFixture.AutoMoq;

namespace Zed.Test.NUnit {
    /// <summary>
    /// Provides factory methods for creating pre-configured <see cref="IFixture"/> instances.
    /// </summary>
    public static class AutoFixtureFactory {

        /// <summary>
        /// Creates an <see cref="IFixture"/> configured with <see cref="AutoMoqCustomization"/>
        /// to auto-mock interfaces and abstract classes, and <see cref="SupportMutableValueTypesCustomization"/>
        /// to support mutable value types.
        /// </summary>
        /// <returns>A configured <see cref="IFixture"/> instance.</returns>
        public static IFixture CreateAutoMockFixture()
            => new Fixture()
                .Customize(new AutoMoqCustomization())
                .Customize(new SupportMutableValueTypesCustomization());
    }
}