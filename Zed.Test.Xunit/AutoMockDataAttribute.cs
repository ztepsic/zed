using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Zed.Test.Xunit {
    /// <summary>
    /// An xUnit data attribute that provides auto-generated, auto-mocked test data
    /// using <see cref="AutoFixture"/> with <see cref="AutoMoqCustomization"/> and
    /// <see cref="SupportMutableValueTypesCustomization"/>.
    /// </summary>
    public class AutoMockDataAttribute : AutoDataAttribute {

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMockDataAttribute"/> class
        /// using a fixture factory that creates an auto-mock fixture.
        /// </summary>
        public AutoMockDataAttribute() : base(() => AutoFixtureFactory.CreateAutoMockFixture()) { }
    }
}
