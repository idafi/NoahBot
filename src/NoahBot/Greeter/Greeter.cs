using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace NoahBot
{
	/// <summary>
	/// Sends a randomly-selected greeting message when joining a Discord guild, or on command.
	/// </summary>
	public class Greeter : IBotCommand
	{
		readonly string[] greetings;

		/// <inheritdoc />
		public string Name
		{
			get { return "Say Hi"; }
		}
		
		/// <inheritdoc />
		public string Pattern
		{
			get { return @"^say hi(\.?|\!*)$"; }
		}
		
		/// <summary>
		/// Constructs and initializes a new <see cref="Greeter"/>.
		/// </summary>
		/// <param name="client">The client connection whose guild-join events will be used.</param>
		/// <param name="greetings">The set of greetings to use.</param>
		public Greeter(DiscordClient client, string[] greetings)
		{
			Assert.Ref(client);
			Assert.Ref(greetings);

			this.greetings = greetings;
			
			client.GuildAvailable += Hello;
			client.GuildMemberAdded += Welcome;
		}
		
		/// <inheritdoc />
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