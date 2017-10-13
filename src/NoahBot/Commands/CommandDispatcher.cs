using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace NoahBot
{
	public class CommandDispatcher
	{
		// TODO: move to config, support multiple permutations
		const string triggerPattern = @"^(hey )?noah\, ";
		const string parseFailMessage = "heh";
		
		readonly List<IBotCommand> commands;
		
		public CommandDispatcher()
		{
			commands = new List<IBotCommand>();
		}
		
		public CommandDispatcher(params IBotCommand[] commands) : this()
		{
			AddCommands(commands);
		}
		
		public void AddCommands(params IBotCommand[] commands)
		{
			Assert.Ref(commands);
			
			this.commands.AddRange(commands);
		}
		
		public void RemoveCommands(params IBotCommand[] commands)
		{
			Assert.Ref(commands);
			
			foreach(IBotCommand cmd in commands)
			{
				if(!this.commands.Remove(cmd))
				{ Log.Warning("tried to remove a bot command that wasn't yet added"); }
			}
		}
		
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