using Newtonsoft.Json;

namespace NoahBot
{
	/// <summary>
	/// Initial settings for the <see cref="ActivitySelector"/>.
	/// </summary>
	public struct ActivitySettings
	{
		/// <summary>
		/// How much to increment or decrement an <see cref="Activity"/>'s weight upon selection.
		/// </summary>
		[JsonProperty]
		public readonly float SelectionModifier;
		
		/// <summary>
		/// A list of potential <see cref="Activity"/> entries.
		/// </summary>
		[JsonProperty]
		public readonly Activity[] Activities;
		
		/// <summary>
		/// Constructs a new <see cref="ActivitySettings"/> instance.
		/// </summary>
		/// <param name="selectMod">The <see cref="SelectionModifier"/> to use.</param>
		public ActivitySettings(float selectMod)
		{
			SelectionModifier = selectMod;
			Activities = new Activity[0];
		}
	};
}