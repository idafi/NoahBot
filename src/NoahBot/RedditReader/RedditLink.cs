using System;
using System.Text.RegularExpressions;

namespace NoahBot
{
	/// <summary>
	/// Represents a link from a subreddit page.
	/// <para>Contains both the direct URI and associated metadata, such as whether or not the link is stickied.</para>
	/// </summary>
	public class RedditLink
	{
		/// <summary>
		/// The URI to which the link is pointing.
		/// </summary>
		public readonly Uri Link;
	
		/// <summary>
		/// The title (main text) of the link.
		/// </summary>
		public readonly string Title;

		/// <summary>
		/// The user who authored the link.
		/// </summary>
		public readonly string Author;
	
		/// <summary>
		/// The time at which the link was posted.
		/// </summary>
		public readonly DateTime Timestamp;

		/// <summary>
		/// The net score for the link. <para>(This may not be fully up-to-date.)</para>
		/// </summary>
		public readonly int Score;

		/// <summary>
		/// Whether or not the link is stickied.
		/// </summary>
		public readonly bool IsStickied;

		/// <summary>
		/// What kind of media the link is pointing to.
		/// </summary>
		public readonly RedditLinkType LinkType;

		/// <summary>
		/// Constructs a new link based on the given string data.
		/// <para>Most of these fields will (attempt to) be parsed into more appropriate representations.</para>
		/// If the links are being properly extracted, this should work out fine.
		/// </summary>
		/// <param name="link">The URL to which the link is pointing.</param>
		/// <param name="title">The title (main text) of the link.</param>
		/// <param name="author">The user who authored the link.</param>
		/// <param name="isStickied">Whether or not the link is stickied. <para>Should == "stickied" if it is.</para></param>
		/// <param name="time">The time at which the link was posted, in <see cref="DateTime"/>-parseable format.</param>
		/// <param name="score">The net score for the link. <para>A bullseye (•) will translate to 0.</para></param>
		public RedditLink(string link, string title, string author, string isStickied, string time, string score)
		{
			Link = new Uri(link.StartsWith("/r/") ? "https://www.reddit.com" + link : link);

			Title = title.Replace("&quot;", "\"");
			Author = author;

			Timestamp = DateTime.Parse(time);
			Score = int.Parse(score.Replace("&bull;", "0"));

			IsStickied = (isStickied == "stickied");
			LinkType = ParseLinkType(Link.ToString());
		}

		/// <summary>
		/// Formats the <see cref="RedditLink"/> data into a multi-line string representation.
		/// </summary>
		/// <returns>The formatted string.</returns>
		public override string ToString()
		{
			return $"{Timestamp}" + ((IsStickied) ? "(Stickied)\n" : "\n") +
				$"({Score}) {Title}\n" +
				$"{Link}\n" +
				$"{Author}";
		}
		
		RedditLinkType ParseLinkType(string link)
		{
			foreach(string pattern in RedditLinkSyntax.ImageLinkPatterns)
			{
				if(Regex.Match(link, pattern).Success)
				{
					return RedditLinkType.Image;
				}
			}

			foreach(string pattern in RedditLinkSyntax.VideoLinkPatterns)
			{
				if(Regex.Match(link, pattern).Success)
				{
					return RedditLinkType.Video;
				}
			}

			return RedditLinkType.Normal;
		}
	};
}