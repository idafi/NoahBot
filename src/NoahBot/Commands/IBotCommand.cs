using System.Threading.Tasks;

namespace NoahBot
{
	public interface IBotCommand
	{
		string Name { get; }
		string Pattern { get; }
		
		Task Execute(CommandData data);
	};
}