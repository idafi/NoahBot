using System;
using System.IO;
using DSharpPlus;
using Newtonsoft.Json;

namespace NoahBot
{
	internal static class NoahBot
	{
		static readonly string[] proverbs =
		{
			"Anyone who teases you loves you.",
			"Don't make a fence more expensive or more important than what it is fencing.",
			"If you have money, men think you are wise, handsome, and able to sing like a bird.",
			"Locks keep out only the honest.",
			"Never trust people who tell you all their troubles but keep from you all their joys.",
			"People make plans, and God laughs.",
			"Pray that you will never have to bear all that you are able to endure.",
			"The only truly dead are those who have been forgotten.",
			"The person who only accepts friends without faults will never have any real friends.",
			"Truth is the safest lie.",
			"Whoever does not try, does not learn.",
			"Your friend has a friend; don't tell him."
		};
		
		static void Main(string[] args)
		{
			ConsoleLogger cLog = new ConsoleLogger();
			FileLogger fLog = new FileLogger("log.txt");
			Log.AddLogger(cLog, LogLevel.Debug);
			Log.AddLogger(fLog, LogLevel.Warning);
			
			try
			{	
				BotConfig config = LoadConfig();
				Bot bot = new Bot(config);

				PrintLogo();
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
		
		static BotConfig LoadConfig()
		{
			string configJson = File.ReadAllText("../data/config.json");
			BotConfig config = JsonConvert.DeserializeObject<BotConfig>(configJson);

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