using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace NoahBot
{
	/// <summary>
	/// Represents a Discord bot.
	/// <para>All NoahBot systems are available to the bot.</para>
	/// </summary>
	public class Bot
	{
		readonly DiscordClient client;
		readonly CommandDispatcher commands;
		
		readonly Greeter greeter;
		readonly RedditReader redditReader;
		readonly ActivitySelector activitySelector;
		
		/// <summary>
		/// Constructs and initializes a new <see cref="Bot"/>, using the given configuration settings.
		/// </summary>
		/// <param name="config">The configuration settings for the discord client.</param>
		public Bot(DiscordConfiguration config)
		{
			Assert.Ref(config);
			
			client = new DiscordClient(config);
			commands = new CommandDispatcher();
			
			greeter = new Greeter(client);
			redditReader = new RedditReader();
			activitySelector = new ActivitySelector();
			
			commands.AddCommands(greeter, redditReader, activitySelector);
		}
		
		/// <summary>
		/// Connects the <see cref="Bot"/> to Discord.
		/// </summary>
		public async Task Connect()
		{
			RegisterEvents();
			
			Log.Note("connecting to Discord...");
			await client.ConnectAsync();
			Log.Note("connected");
		}
		
		/// <summary>
		/// Disconnects the <see cref="Bot"/> from Discord.
		/// </summary>
		public async Task Disconnect()
		{
			UnregisterEvents();
			
			Log.Note("disconnecting from Discord...");
			await client.DisconnectAsync();
			Log.Note("disconnected");
		}
		
		void RegisterEvents()
		{
			client.SocketClosed += SocketClosed;
			client.MessageCreated += MessageCreated;
		}
		
		void UnregisterEvents()
		{
			client.SocketClosed -= SocketClosed;
			client.MessageCreated -= MessageCreated;
		}
		
		async Task SocketClosed(SocketCloseEventArgs e)
		{
			await Task.Run(() => 
			{
				Log.Error("socket connection was closed unexpectedly");
			});
		}
		
		async Task MessageCreated(MessageCreateEventArgs e)
		{
			await commands.TryCommand(e.Message);
		}
	};
}