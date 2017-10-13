using System.Text.RegularExpressions;
using DSharpPlus.Entities;

namespace NoahBot
{
	public struct CommandData
	{
		public readonly DiscordMessage Message;
		public readonly GroupCollection Captures;
		
		public CommandData(DiscordMessage message, GroupCollection captures)
		{
			Message = message;
			Captures = captures;
		}
	};
}