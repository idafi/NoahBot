using DSharpPlus;
using Newtonsoft.Json;

namespace NoahBot
{
	/// <summary>
	/// Configuration settings for a <see cref="Bot"/>.
	/// </summary>
	public struct BotConfig
	{
		/// <summary>
		/// Discord API configuration, most notably the bot's token.
		/// </summary>
		[JsonProperty]
		public DiscordConfiguration DiscordConfig;

		/// <summary>
		/// The set of random greetings used by the <see cref="Greeter"/> system.
		/// </summary>
		[JsonProperty]
		public string[] Greetings;

		/// <summary>
		/// Settings and activities for the <see cref="ActivitySelector"/>.
		/// </summary>
		[JsonProperty]
		public ActivitySettings ActivityConfig;

		/// <summary>
		/// Constructs a new BotConfig.
		/// </summary>
		/// <param name="discordConfig">The DiscordConfiguration to use.</param>
		/// <param name="greetings">The greetings for the <see cref="Greeter"/> to use.</param>
		/// <param name="activityConfig">The settings for the <see cref="ActivitySelector"/>.</param>
		public BotConfig(DiscordConfiguration discordConfig, string[] greetings, ActivitySettings activityConfig)
		{
			DiscordConfig = new DiscordConfiguration();
			Greetings = new string[0];
			ActivityConfig = new ActivitySettings();
		}
	};
}