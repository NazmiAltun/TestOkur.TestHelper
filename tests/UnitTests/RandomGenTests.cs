using FluentAssertions;
using TestOkur.TestHelper;
using Xunit;

namespace UnitTests
{
	public class RandomGenTests
	{
		[Fact]
		public void RandomString_Should_GenerateString_WithProvidedLength()
		{
			RandomGen.String(10).Should().HaveLength(10);
		}

		[Fact]
		public void Phone_Should_GenerateAValidPhone()
		{
			RandomGen.Phone().Should().HaveLength(10)
				.And.Subject.Should().StartWith("5");
		}
	}
}
