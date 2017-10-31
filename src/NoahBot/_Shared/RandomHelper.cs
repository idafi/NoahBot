using System;

namespace NoahBot
{
	public static class RandomHelper
	{
		static readonly Random rand;
		
		static RandomHelper()
		{
			rand = new Random();
		}
		
		public static int Index(int max)
		{
			Assert.Sign(max);
			
			return rand.Next(max);
		}
		
		public static int Int()
		{
			return rand.Next();
		}
		
		public static double Double()
		{
			return rand.NextDouble();
		}
		
		public static T ArraySelect<T>(T[] array)
		{
			Assert.Ref(array);
			
			return array[Index(array.Length)];
		}
	};
}