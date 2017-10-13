using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace NoahBot
{
	public class Greeter : IBotCommand
	{
		static readonly string[] greetings =
		{
			"Helllooooo!",
			"Oh hai!"
		};
		
		public string Name
		{
			get { return "Say Hi"; }
		}
		
		public string Pattern
		{
			get { return @"^say hi(\.?|\!*)$"; }
		}
		
		public Greeter(DiscordClient client)
		{
			Assert.Ref(client);
			
			client.GuildAvailable += Hello;
			client.GuildMemberAdded += Welcome;
		}
		
		public async Task Execute(CommandData data)
		{
			string greeting = RandomHelper.ArraySelect<string>(greetings);
			await data.Message.RespondAsync(greeting, false, null);
		}
		
		async Task Hello(GuildCreateEventArgs e)
		{
			Log.Note($"entered a guild, named '{e.Guild.Name}'. saying hello...");
			
			DiscordChannel channel = e.Guild.GetDefaultChannel();
			string greeting = RandomHelper.ArraySelect<string>(greetings);
			
			await channel.SendMessageAsync(greeting, false, null);
		}
		
		async Task Welcome(GuildMemberAddEventArgs e)
		{
			Log.Note($"detected newly invited member '{e.Member.Nickname}', " +
				$"in guild '{e.Guild.Name}'. saying welcome...");
			
			DiscordChannel channel = e.Guild.GetDefaultChannel();
			string name = e.Member.Nickname;
			
			await channel.SendMessageAsync($"Welcome {name}!", false, null);
		}
	};
}