namespace NoahBot
{
	/// <summary>
	/// Contains the regex patterns used by the <see cref="RedditLinkExtractor"/>. 
	/// </summary>
	public static class RedditLinkSyntax
	{
		const string sourcePrefix = @"<div class=\"" thing id-.*?";

		/// <summary>
		/// Pattern matching the links listing in the subreddit page.
		/// </summary>
		public const string SourcePattern = sourcePrefix + @"<div class=\""clearleft\""></div>";

		/// <summary>
		/// Pattern matching a link's URL destination.
		/// </summary>
		public const string LinkPattern = @"data-url=\""(.*?)\""";

		/// <summary>
		/// Pattern matching a link's title content (main text.)
		/// </summary>
		public const string TitlePattern = @"a class=\""title.*?>(.*?)</a>";

		/// <summary>
		/// Pattern matching the username of the link's author.
		/// </summary>
		public const string AuthorPattern = @"class=\""author.*?>(.*?)</a>";

		/// <summary>
		/// Pattern which will match if the link is stickied.
		/// </summary>
		public const string StickiedPattern = sourcePrefix + @"(stickied)";

		/// <summary>
		/// Pattern matching the link's timestamp.
		/// </summary>
		public const string TimestampPattern = @"datetime=\""(.*?)\""";

		/// <summary>
		/// Pattern matching the link's net score.
		/// </summary>
		public const string ScorePattern = @"score unvoted.*?>(.*?)<";

		/// <summary>
		/// Patterns detecting whether a link points to a valid image format.
		/// </summary>
		public static readonly string[] ImageLinkPatterns = new string[]
		{
			@".*\.jpg$",
			@".*\.png$",
			@".*\.bmp$",
			@".*\.gif$"
		};

		/// <summary>
		/// Patterns detecting whether a link points to a valid video format.
		/// </summary>
		public static readonly string[] VideoLinkPatterns = new string[]
		{
			@".*\.webm$",
			@".*\.gifv$",
			@"youtube\.com/watch\?v=",
			@"youtu\.be/"
		};
	};
}