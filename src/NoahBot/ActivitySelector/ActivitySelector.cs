using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NoahBot
{
	class Activity
	{
		[JsonProperty]
		public readonly string Name;
		
		[JsonProperty]
		public readonly float Weight;

		public Activity()
		{
			Name = "?";
			Weight = 0;
		}
		
		public Activity(string name)
		{
			Name = name;
			Weight = 0;
		}
	};
	
	struct ActivitySettings
	{
		[JsonProperty]
		public readonly float SelectionModifier;
		
		[JsonProperty]
		public readonly Activity[] Activities;
		
		public ActivitySettings(float selectMod)
		{
			SelectionModifier = selectMod;
			Activities = new Activity[0];
		}
	};
	
	public class ActivitySelector : IBotCommand
	{
		readonly ActivitySettings settings;
		readonly float[] weights;
		
		public string Name
		{
			get { return "Say What to Do"; }
		}
		
		public string Pattern
		{
			get { return @"^(tell us what to do(\.?|\!*)|what should we do\?*)$"; }
		}
		
		public ActivitySelector()
		{
			string json = File.ReadAllText("activities.json");
			settings = JsonConvert.DeserializeObject<ActivitySettings>(json);
			
			weights = new float[settings.Activities.Length];
			for(int i = 0; i < weights.Length; i++)
			{ weights[i] = settings.Activities[i].Weight; }
		
			ValidateWeights();
		}
		
		public async Task Execute(CommandData data)
		{
			string act = SelectActivity();
			await data.Message.RespondAsync(act, false, null);
		}

		string SelectActivity()
		{
			float rand = (float)(RandomHelper.Double());

			int selection = -1;
			for(int i = 0; i < weights.Length; i++)
			{
				if(rand < weights[i])
				{
					selection = i;
					break;
				}

				rand -= weights[i];
			}

			Reweight(selection);
			return settings.Activities[selection].Name;
		}

		void ValidateWeights()
		{
			float sum = 0;
			foreach(float f in weights)
			{ sum += f; }
			
			if(sum > 1)
			{
				Log.Warning("total probability of all activity weights exceeds 1");
				
				float mod = (sum - 1) / weights.Length;
				for(int i = 0; i < weights.Length; i++)
				{ weights[i] -= mod; }
			}
			else if(sum < 1)
			{
				Log.Warning("total probability of all activity weights is less than 1");
				
				float mod = (1 - sum) / weights.Length;
				for(int i = 0; i < weights.Length; i++)
				{ weights[i] += mod; }
			}
		}

		void Reweight(int selected)
		{
			Assert.Index(selected, weights.Length);

			float subMod = Math.Min(weights[selected], settings.SelectionModifier);
			float addMod = subMod / weights.Length;
			for(int i = 0; i < weights.Length; i++)
			{
				if(i == selected)
				{ weights[i] -= subMod; }
				else
				{ weights[i] += addMod; }
			}
		}
	};
}