using System.Threading.Tasks;

namespace NoahBot
{
	/// <summary>
	/// Represents a string-based, end-user bot command, controlled by the <see cref="CommandDispatcher"/>.
	/// </summary>
	public interface IBotCommand
	{
		/// <summary>
		/// This <see cref="IBotCommand"/>'s formal name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The regular expression pattern which, when matched, triggers this <see cref="IBotCommand"/>.
		/// <para>Any captured regex substrings will be sent to <see cref="Execute(CommandData)"/> through
		/// the <see cref="CommandData"/> structure.</para>
		/// </summary>
		string Pattern { get; }
		
		/// <summary>
		/// Executes this <see cref="IBotCommand"/>.
		/// </summary>
		/// <param name="data"><see cref="CommandData"/> usable by the <see cref="IBotCommand"/>.</param>
		Task Execute(CommandData data);
	};
}