namespace TestOkur.TestHelper.Extensions
{
	using System;
	using System.Linq;

	public static class RandomExtensions
	{
		public static string RandomString(this Random random, int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
