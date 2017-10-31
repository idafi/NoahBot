using System;

namespace NoahBot
{
	/// <summary>
	/// An <see cref="ILogger"/> implementation that logs messages to the program console.
	/// <para>Messages will be color-coded based on severity.</para>
	/// </summary>
	public class ConsoleLogger : ILogger
	{
		/// <inheritdoc />
		public void Write(LogLevel level, string msg)
		{
			Assert.Ref(msg);
			
			SetColor(level);
			Console.WriteLine(msg.ToString());

			// revert color after write
			SetColor(LogLevel.Note);
		}

		void SetColor(LogLevel level)
		{
			switch(level)
			{
				case LogLevel.Debug:
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Note:
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Failure:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.BackgroundColor = ConsoleColor.Red;
					break;
			}
		}
	};
}