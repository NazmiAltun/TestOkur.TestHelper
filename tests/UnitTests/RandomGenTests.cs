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
	}
}
