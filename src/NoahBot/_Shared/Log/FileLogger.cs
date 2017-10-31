using System;
using System.IO;

namespace NoahBot
{
	/// <summary>
	/// An <see cref="ILogger"/> implementation that logs messages to an output file.
	/// </summary>
	public class FileLogger : IDisposable, ILogger
	{
		readonly StreamWriter writer;

		/// <summary>
		/// Creates a new <see cref="FileLogger"/>, opening an output file at the given path.
		/// </summary>
		/// <param name="filePath">The path at which to open the output file.</param>
		public FileLogger(string filePath)
		{
			FileStream stream = null;

			try
			{
				stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
				writer = new StreamWriter(stream);
			}
			catch(Exception e)
			{
				Log.Error("failed to open FileLogger\n" + e.ToString());
				
				if(writer != null)
				{ writer.Dispose(); }
				else if(stream != null)
				{ stream.Dispose(); }
			}
		}

		/// <summary>
		/// Closes the output file used by this <see cref="FileLogger"/>.
		/// </summary>
		public void Dispose()
		{
			writer?.Dispose();
		}

		/// <inheritdoc />
		public void Write(LogLevel level, string msg)
		{
			if(writer != null)
			{
				try
				{ writer.WriteLine(msg); }
				catch(Exception e)
				{ Log.Error("couldn't log to file\n" + e.ToString()); }
			}
			else
			{ Log.Error("couldn't log to file: no stream is open"); }
		}
	};
}