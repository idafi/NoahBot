using System.Text.RegularExpressions;
using DSharpPlus.Entities;

namespace NoahBot
{
	/// <summary>
	/// Contains data used to execute an <see cref="IBotCommand"/>.
	/// </summary>
	public struct CommandData
	{
		/// <summary>
		/// The message which contained the <see cref="IBotCommand"/>'s trigger pattern.
		/// </summary>
		public readonly DiscordMessage Message;

		/// <summary>
		/// All regex substrings captured when the <see cref="IBotCommand"/>'s trigger pattern was parsed.
		/// </summary>
		public readonly GroupCollection Captures;
		
		/// <summary>
		/// Constructs a new <see cref="CommandData"/> instance.
		/// </summary>
		/// <param name="message">The message containing the trigger pattern.</param>
		/// <param name="captures">Regex substrings captured from the message.</param>
		public CommandData(DiscordMessage message, GroupCollection captures)
		{
			Message = message;
			Captures = captures;
		}
	};
}