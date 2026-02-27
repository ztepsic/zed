using AutoFixture.NUnit3;

namespace Zed.Test.NUnit {
    /// <summary>
    /// An NUnit data attribute that combines inline argument values with auto-generated,
    /// auto-mocked test data using the fixture created by <see cref="AutoMockDataAttribute.CreateAutoMockFixture"/>.
    /// </summary>
    /// <param name="values">The inline argument values passed to the test method.</param>
    public class InlineAutoMockDomainDataAttribute(params object[] values)
        : InlineAutoDataAttribute(() => AutoFixtureFactory.CreateAutoMockFixture(), values) {
    }
}
