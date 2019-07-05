namespace UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using TestOkur.TestHelper.Extensions;
	using Xunit;

	public class EnumerableExtensionTests
	{
		[Fact]
		public void RandomShouldReturnElementFromTheList()
		{
			var list = new List<int> {67, 34, 87, 12, 5, 57};
			list.Contains(list.Random()).Should().BeTrue();
		}
	}
}
