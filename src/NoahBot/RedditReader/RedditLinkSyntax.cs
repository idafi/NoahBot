namespace NoahBot
{
	/// <summary>
	/// Contains the regex patterns used by the <see cref="RedditLinkExtractor"/>. 
	/// </summary>
	public static class RedditLinkSyntax
	{
		const string sourcePrefix = @"<div class=\"" thing id-.*?";

		public const string SourcePattern = sourcePrefix + @"<div class=\""clearleft\""></div>";
		public const string LinkPattern = @"data-url=\""(.*?)\""";
		public const string TitlePattern = @"a class=\""title.*?>(.*?)</a>";
		public const string AuthorPattern = @"class=\""author.*?>(.*?)</a>";
		public const string StickiedPattern = sourcePrefix + @"(stickied)";
		public const string TimestampPattern = @"datetime=\""(.*?)\""";
		public const string ScorePattern = @"score unvoted.*?>(.*?)<";

		public static readonly string[] ImageLinkPatterns = new string[]
		{
			@".*\.jpg$",
			@".*\.png$",
			@".*\.bmp$",
			@".*\.gif$"
		};

		public static readonly string[] VideoLinkPatterns = new string[]
		{
			@".*\.webm$",
			@".*\.gifv$",
			@"youtube\.com/watch\?v=",
			@"youtu\.be/"
		};
	};
}