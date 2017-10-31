using System;
using System.IO;
using DSharpPlus;
using Newtonsoft.Json;

namespace NoahBot
{
	internal static class NoahBot
	{
		static string[] proverbs;
		
		static void Main(string[] args)
		{
			ConsoleLogger cLog = new ConsoleLogger();
			FileLogger fLog = new FileLogger("log.txt");
			Log.AddLogger(cLog, LogLevel.Debug);
			Log.AddLogger(fLog, LogLevel.Warning);
			
			try
			{
				string json = File.ReadAllText("proverbs.json");
				proverbs = JsonConvert.DeserializeObject<string[]>(json);
				
				PrintLogo();
				
				DiscordConfiguration config = LoadConfiguration();
				Bot bot = new Bot(config);
				RunBot(bot);
			}
			catch(Exception e)
			{
				Log.Failure(e.ToString());
				
				Log.Note("press Enter to exit...");
				Console.ReadLine();
			}
			
			fLog.Dispose();
		}
		
		static void PrintLogo()
		{
			string proverb = RandomHelper.ArraySelect<string>(proverbs);
			Console.WriteLine($"NoahBot v0.0.1\n\"{proverb}\"\n");
		}
		
		static DiscordConfiguration LoadConfiguration()
		{
			string json = File.ReadAllText("config.json");
			DiscordConfiguration config = JsonConvert.DeserializeObject<DiscordConfiguration>(json);
			
			return config;
		}
		
		static void RunBot(Bot bot)
		{
			bot.Connect().Wait();
			
			Console.WriteLine("\npress Enter at any time to disconnect\n");
			Console.ReadLine();
			
			bot.Disconnect().Wait();
		}
	};
}