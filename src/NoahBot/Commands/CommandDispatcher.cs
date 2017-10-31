using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace NoahBot
{
	/// <summary>
	/// Checks incoming messages for <see cref="IBotCommand"/> trigger patterns, executing them if found.
	/// </summary>
	public class CommandDispatcher
	{
		// TODO: move to config, support multiple permutations
		const string triggerPattern = @"^(hey )?noah\, ";
		const string parseFailMessage = "heh";
		
		readonly List<IBotCommand> commands;
		
		/// <summary>
		/// Constructs a new <see cref="CommandDispatcher"/>.
		/// </summary>
		public CommandDispatcher()
		{
			commands = new List<IBotCommand>();
		}
		
		/// <summary>
		/// Constructs a new <see cref="CommandDispatcher"/>, registering the given <see cref="IBotCommand"/>s.
		/// </summary>
		/// <param name="commands"></param>
		public CommandDispatcher(params IBotCommand[] commands) : this()
		{
			AddCommands(commands);
		}
		
		/// <summary>
		/// Registers the given <see cref="IBotCommand"/>s with the <see cref="CommandDispatcher"/>.
		/// </summary>
		/// <param name="commands">The <see cref="IBotCommand"/>s to register.</param>
		public void AddCommands(params IBotCommand[] commands)
		{
			Assert.Ref(commands);
			
			this.commands.AddRange(commands);
		}
		
		/// <summary>
		/// Unregisters the given <see cref="IBotCommand"/>s from the <see cref="CommandDispatcher"/>.
		/// </summary>
		/// <param name="commands">The <see cref="IBotCommand"/>s to unregister.</param>
		public void RemoveCommands(params IBotCommand[] commands)
		{
			Assert.Ref(commands);
			
			foreach(IBotCommand cmd in commands)
			{
				if(!this.commands.Remove(cmd))
				{ Log.Warning("tried to remove a bot command that wasn't yet added"); }
			}
		}
		
		/// <summary>
		/// Unregisters the given <see cref="IBotCommand"/>s from the <see cref="CommandDispatcher"/>,
		/// using their formal names as defined by <see cref="IBotCommand.Name"/>.
		/// </summary>
		/// <param name="names">The names of the <see cref="IBotCommand"/>s to unregister.</param>
		public void RemoveCommands(params string[] names)
		{
			foreach(string name in names)
			{
				int i = -1;
				do
				{
					i = commands.FindIndex(c => c.Name == name);
					if(i > -1)
					{ commands.RemoveAt(i); }
				}
				while(i > -1);
			}
		}
		
		/// <summary>
		/// Checks the given message for an <see cref="IBotCommand"/> trigger pattern, executing it if found.
		/// </summary>
		/// <param name="message">The message to check.</param>
		public async Task TryCommand(DiscordMessage message)
		{
			Assert.Ref(message);
			
			string cmdStr = ParseCommand(message.Content);
			if(cmdStr != null)
			{
				Log.Note("detected command trigger...");
				
				foreach(IBotCommand cmd in commands)
				{
					Match m = Regex.Match(cmdStr, cmd.Pattern, RegexOptions.IgnoreCase);
					
					if(m.Success)
					{
						Log.Note("...and found a matching command: " + cmd.Name);
						
						CommandData data = new CommandData(message, m.Groups);
						await cmd.Execute(data);
						
						return;
					}
				}
				
				Log.Note("...but couldn't find a matching command");
				await message.RespondAsync(parseFailMessage, false, null);
			}
		}
		
		string ParseCommand(string source)
		{
			Assert.Ref(source);
			
			Match trg = Regex.Match(source, triggerPattern, RegexOptions.IgnoreCase);

			if(trg.Success)
			{
				int cmdPos = trg.Index + trg.Length;

				if(source.Length > cmdPos)
				{ return source.Substring(cmdPos); }
				else
				{ Log.Note("detected a command trigger, but no command was provided"); }
			}

			return null;
		}
	};
}