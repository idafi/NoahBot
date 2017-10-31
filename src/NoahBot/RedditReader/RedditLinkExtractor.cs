using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NoahBot
{
	/// <summary>
	/// Extracts links from a subreddit page's source text, and parses them into an array of <see cref="RedditLink"/>s.
	/// <para>Currently, you're getting everything inside the source text. A means to cap the number of parsed links
	/// is feasible, but pending.</para>
	/// </summary>
	public class RedditLinkExtractor
	{
		/// <summary>
		/// Parse <see cref="RedditLink"/>s from the given subreddit page's source text. 
		/// </summary>
		/// <param name="pageSource">The source text to be parsed.</param>
		/// <returns>An array of <see cref="RedditLink"/>s parsed from the source text.</returns>
		public RedditLink[] ExtractLinks(string pageSource)
		{
			return ParseLinks(ExtractLinkSource(pageSource));
		}
	
		RedditLink[] ParseLinks(IReadOnlyList<string> sources)
		{
			Log.Debug("parsing links");
			RedditLink[] links = new RedditLink[sources.Count];
		
			for(int i = 0; i < sources.Count; i++)
			{
				string src = sources[i];

				string link = ExtractFromMatch(Regex.Match(src, RedditLinkSyntax.LinkPattern), 1);
				string title = ExtractFromMatch(Regex.Match(src, RedditLinkSyntax.TitlePattern), 1);
				string author = ExtractFromMatch(Regex.Match(src, RedditLinkSyntax.AuthorPattern), 1);
				string stickied = ExtractFromMatch(Regex.Match(src, RedditLinkSyntax.StickiedPattern), 1);
				string time = ExtractFromMatch(Regex.Match(src, RedditLinkSyntax.TimestampPattern), 1);
				string score = ExtractFromMatch(Regex.Match(src, RedditLinkSyntax.ScorePattern), 1);
			
				links[i] = new RedditLink(link, title, author, stickied, time, score);
			}
		
			return links;
		}

		IReadOnlyList<string> ExtractLinkSource(string pageSource)
		{
			Log.Note("extracting links from page source");

			MatchCollection m = Regex.Matches(pageSource, RedditLinkSyntax.SourcePattern);
			Log.Debug($"found {m.Count} links");

			string[] links = new string[m.Count];
			for(int i = 0; i < m.Count; i++)
			{
				links[i] = m[i].Groups[0].Value;
			}
		
			return links;
		}

		string ExtractFromMatch(Match match, int group)
		{
			if(match.Success && match.Groups.Count > group)
			{
				return match.Groups[group].Value;
			}

			return "";
		}
	};
}