namespace TestOkur.TestHelper
{
	using System;
	using System.Threading;
	using TestOkur.TestHelper.Extensions;

	public static class RandomGen
	{
		private static Random _global = new Random();
		private static ThreadLocal<Random> _local = new ThreadLocal<Random>(() =>
		{
			int seed;
			lock (_global)
			{
				seed = _global.Next();
			}

			return new Random(seed);
		});

		public static int Next(int maxValue) => _local.Value.Next(maxValue);

		public static int Next() => _local.Value.Next();

		public static string String(int len) => _local.Value.RandomString(len);
	}
}
