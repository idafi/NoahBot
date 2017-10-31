using System;

namespace NoahBot
{
	/// <summary>
	/// Provides helper shortcuts for random-number generation, using an internally
	/// managed <see cref="System.Random"/> instance.
	/// </summary>
	public static class RandomHelper
	{
		static readonly Random rand;
		
		static RandomHelper()
		{
			rand = new Random();
		}
		
		/// <summary>
		/// Generates a random index, greater than -1 and less than the given maximum.
		/// </summary>
		/// <param name="max">The exclusive max bound of the generated number.</param>
		/// <returns>An integer greater than -1 and les than the max bound.</returns>
		public static int Index(int max)
		{
			Assert.Sign(max);
			
			return rand.Next(max);
		}
		
		/// <summary>
		/// Generates an unbounded random integer.
		/// </summary>
		/// <returns>An unbounded random integer.</returns>
		public static int Int()
		{
			return rand.Next();
		}
		
		/// <summary>
		/// Generates a double-precision floating point number between 0 and 1.
		/// </summary>
		/// <returns>A double-precision floating point number between 0 and 1.</returns>
		public static double Double()
		{
			return rand.NextDouble();
		}
		
		/// <summary>
		/// Randomly selects an element from the given array.
		/// </summary>
		/// <typeparam name="T">The array's element type.</typeparam>
		/// <param name="array">The array from which to select.</param>
		/// <returns>A random element selectd from the array.</returns>
		public static T ArraySelect<T>(T[] array)
		{
			Assert.Ref(array);
			
			return array[Index(array.Length)];
		}
	};
}