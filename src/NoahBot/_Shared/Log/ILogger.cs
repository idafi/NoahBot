namespace NoahBot
{
	/// <summary>
	/// Represents an output destination for <see cref="Log"/> messages.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Writes a log message through this <see cref="ILogger"/>.
		/// </summary>
		/// <param name="level">The severity level of the message.</param>
		/// <param name="msg">The message string to write.</param>
		void Write(LogLevel level, string msg);
	};
}