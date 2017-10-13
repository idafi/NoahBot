using System;
using System.Text.RegularExpressions;

namespace NoahBot
{
	public class RedditLink
	{
		public readonly Uri Link;
		
		public readonly string Title;
		public readonly string Author;
		public readonly DateTime Timestamp;
		
		public readonly int Score;
		
		public readonly bool IsStickied;
		public readonly RedditLinkType LinkType;

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

		/// <inheritdoc />
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