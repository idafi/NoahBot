using System;
using System.IO;

namespace NoahBot
{
	public class FileLogger : IDisposable, ILogger
	{
		readonly StreamWriter writer;

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

		public void Dispose()
		{
			writer?.Dispose();
		}

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