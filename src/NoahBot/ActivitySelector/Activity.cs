using Newtonsoft.Json;

namespace NoahBot
{
	/// <summary>
	/// Represents a probability-weighted activity for selection by the <see cref="ActivitySelector"/>.
	/// </summary>
	public class Activity
	{
		/// <summary>
		/// The name of the <see cref="Activity"/> -- i.e., the message printed to Discord when selected.
		/// </summary>
		[JsonProperty]
		public readonly string Name;
		
		/// <summary>
		/// The starting weight of the <see cref="Activity"/>.
		/// </summary>
		[JsonProperty]
		public readonly float Weight;

		/// <summary>
		/// Constructs a new, empty <see cref="Activity"/>.
		/// </summary>
		public Activity()
		{
			Name = "?";
			Weight = 0;
		}
		
		/// <summary>
		/// Constructs an <see cref="Activity"/> with the given name, and a base weight of 0.
		/// </summary>
		/// <param name="name">The new <see cref="Activity"/>'s name.</param>
		public Activity(string name)
		{
			Name = name;
			Weight = 0;
		}
	};
}