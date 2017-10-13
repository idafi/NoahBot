using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace NoahBot
{
	public class RedditReader : IBotCommand
	{
		const string redditPrefix = "https://www.reddit.com/r/";
		
		public string Name
		{
			get { return "Reddit Reader"; }
		}
		
		public string Pattern
		{
			get { return @"^what's (\w*) ?on r/(\w+)\?*$"; }
		}
		
		public async Task Execute(CommandData data)
		{
			RedditLinkExtractor extractor = new RedditLinkExtractor();

			string subreddit = data.Captures[2].Value;
			string option = data.Captures[1].Value;

			string pageSrc = await GetPageSrc(subreddit, option, data.Message);
			if(pageSrc != "")
			{
				RedditLink[] links = extractor.ExtractLinks(pageSrc);
				foreach(RedditLink l in links)
				{
					if(!l.IsStickied)
					{
						switch(l.LinkType)
						{
							case RedditLinkType.Image:
								Log.Note("first non-stickied link is an image: creating image embed");
								await EmbedResponse(data.Message, l, subreddit, true);
								break;
							case RedditLinkType.Normal:
								Log.Note("first non-stickied link is a normal link: creating text embed");
								await EmbedResponse(data.Message, l, subreddit, false);
								break;
							case RedditLinkType.Video:
								Log.Note("first non-stickied link is a video: posting direct link");
								await data.Message.RespondAsync(l.Link.ToString());
								break;
						}
					}
				}
			}
		}

		async Task<string> GetPageSrc(string subreddit, string option, DiscordMessage message)
		{
			string src = "";
			string urlBase = redditPrefix + subreddit;
			string url = urlBase + $"/{option}/";
			
			Log.Note("retrieving page source for " + url);
			
			// confirm the base path exists
			try
			{
				WebRequest testRequest = WebRequest.Create(urlBase);
				
				Log.Debug("sending test request to " + urlBase);
				using(WebResponse response = await testRequest.GetResponseAsync()) { }
				
				// if that worked out, try the full path
				try
				{
					WebRequest request = WebRequest.Create(url);
					Log.Debug("sending request to " + url);
					
					using(WebResponse response = await request.GetResponseAsync())
					{
						Log.Debug("received a response; retrieving source text");
						
						using(StreamReader reader = new StreamReader(response.GetResponseStream()))
						{ src = await reader.ReadToEndAsync(); }
					}
				}
				catch(Exception e)
				{
					Log.Error($"failed to retrieve page source: category '{option}' not found in r/{subreddit}\n{e}");
					await message.RespondAsync($"There's no category in r/{subreddit} called '{option}'... lol");
				}
			}
			catch(Exception e)
			{
				Log.Error($"failed to retrieve page source: subreddit '{subreddit}' not found\n{e}");
				await message.RespondAsync($"Uh, 'r/{subreddit}' doesn't exist...");
			}

			return src;
		}

		async Task EmbedResponse(DiscordMessage m, RedditLink l, string subreddit, bool isImage)
		{
			string linkString = l.Link.ToString();
			
			DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
			builder.Title = $"{l.Title} - {l.Author} ({l.Score} points)";
			builder.Url = linkString;
			builder.WithAuthor($"r/{subreddit}", redditPrefix + subreddit, null);

			if(isImage)
			{ builder.ImageUrl = linkString; }

			await m.RespondAsync("", false, builder.Build());
		}
	};
}