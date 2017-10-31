namespace NoahBot
{
	/// <summary>
	/// What kind of media a <see cref="RedditLink"/> is pointing to.
	/// </summary>
	public enum RedditLinkType
	{
		/// <summary>
		/// Anything that isn't a direct link to an image or video.
		/// </summary>
		Normal,

		/// <summary>
		/// A direct link to any image matching <see cref="RedditLinkSyntax.ImageLinkPatterns"/>.
		/// </summary>
		Image,

		/// <summary>
		/// A direct link to any video matching <see cref="RedditLinkSyntax.VideoLinkPatterns"/>. 
		/// </summary>
		Video
	};
}