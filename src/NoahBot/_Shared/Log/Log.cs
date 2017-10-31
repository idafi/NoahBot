using System;
using System.Collections.Generic;

namespace NoahBot
{
	/// <summary>
	/// Utility for logging runtime messages.
	/// <para><see cref="ILogger"/> implementors provide an arbitrary number of logging destinations.</para>
	/// A message will only be logged to an <see cref="ILogger"/> if it meets that logger's minimum severity level.
	/// </summary>
	public static class Log
	{
		static readonly Dictionary<ILogger, LogLevel> loggers;
		
		static Log()
		{
			loggers = new Dictionary<ILogger, LogLevel>();
		}
		
		/// <summary>
		/// Shortcut for logging a message at <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="msg">The message string to log.</param>
		public static void Debug(string msg) => Write(LogLevel.Debug, msg);

		/// <summary>
		/// Shortcut for logging a message at <see cref="LogLevel.Note"/> level.
		/// </summary>
		/// <param name="msg">The message string to log.</param>
		public static void Note(string msg) => Write(LogLevel.Note, msg);

		/// <summary>
		/// Shortcut for logging a message at <see cref="LogLevel.Warning"/> level.
		/// </summary>
		/// <param name="msg">The message string to log.</param>
		public static void Warning(string msg) => Write(LogLevel.Warning, msg);

		/// <summary>
		/// Shortcut for logging a message at <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="msg">The message string to log.</param>
		public static void Error(string msg) => Write(LogLevel.Error, msg);

		/// <summary>
		/// Shortcut for logging a message at <see cref="LogLevel.Failure"/> level.
		/// </summary>
		/// <param name="msg">The message string to log.</param>
		public static void Failure(string msg) => Write(LogLevel.Failure, msg);
		
		/// <summary>
		/// Logs a message to all <see cref="ILogger"/>s whose minimum <see cref="LogLevel"/>
		/// is at least as severe the given level.
		/// </summary>
		/// <param name="level">The severity of the logged message.</param>
		/// <param name="msg">The message string.</param>
		public static void Write(LogLevel level, string msg)
		{
			Assert.Ref(msg);
			
			DateTime time = DateTime.Now;
			string timestamp = time.ToString("HH:mm:ss.fff: ");
			msg = timestamp + msg;
			
			foreach(var pair in loggers)
			{
				ILogger logger = pair.Key;
				LogLevel minLevel = pair.Value;
				
				if(level >= minLevel)
				{ logger.Write(level, msg); }
			}
		}
		
		/// <summary>
		/// Registers an <see cref="ILogger"/> with the <see cref="Log"/> system, at the
		/// given minimum severity level.
		/// <para>This <see cref="ILogger"/> will only log messages that meet or exceed this minimum
		/// severity.</para>
		/// </summary>
		/// <param name="logger">The <see cref="ILogger"/> to register.</param>
		/// <param name="minLevel">The minimum severity level for messages to be logged to this <see cref="ILogger"/>.</param>
		public static void AddLogger(ILogger logger, LogLevel minLevel)
		{
			Assert.Ref(logger);
			
			if(!loggers.ContainsKey(logger))
			{ loggers.Add(logger, minLevel); }
			else
			{ Warning("tried to add a logger that's already in use"); }
		}
		
		/// <summary>
		/// Unregisters an <see cref="ILogger"/> currently registered to the <see cref="Log"/> system.
		/// </summary>
		/// <param name="logger">The <see cref="ILogger"/> to unregister.</param>
		public static void RemoveLogger(ILogger logger)
		{
			if(!loggers.Remove(logger))
			{ Warning("tried to remove a logger that isn't in use"); }
		}
		
		/// <summary>
		/// Changes the minimum severity for a registered <see cref="ILogger"/>.
		/// </summary>
		/// <param name="logger">The <see cref="ILogger"/> whose severity level is to be changed.</param>
		/// <param name="minLevel">The new minimum severity level for the <see cref="ILogger"/>.</param>
		public static void ChangeMinLevel(ILogger logger, LogLevel minLevel)
		{
			Assert.Ref(logger);
			
			if(loggers.ContainsKey(logger))
			{ loggers[logger] = minLevel; }
			else
			{ Error("couldn't change min log level: logger isn't in use"); }
		}
	};
}