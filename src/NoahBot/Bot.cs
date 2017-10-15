using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace NoahBot
{
	public class Bot
	{
		readonly DiscordClient client;
		readonly CommandDispatcher commands;
		
		readonly Greeter greeter;
		readonly RedditReader redditReader;
		readonly ActivitySelector activitySelector;
		
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
		
		public async Task Connect()
		{
			RegisterEvents();
			
			Log.Note("connecting to Discord...");
			await client.ConnectAsync();
			Log.Note("connected");
		}
		
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