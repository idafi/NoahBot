namespace NoahBot
{
	public interface ILogger
	{
		void Write(LogLevel level, string msg);
	};
}