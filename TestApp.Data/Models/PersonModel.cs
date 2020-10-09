using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using TestApp.Data.Helpers;
using Newtonsoft.Json;

namespace TestApp.Data.Models
{
	public class PersonModel : TableEntity
	{
		public PersonModel()
		{
			PartitionKey = Constants.TableDBConstant.TEST_APP_PARTITION;
			RowKey = Guid.NewGuid().ToString();
		}

		[JsonProperty("name")]
		public string Name;

		[JsonProperty("age")]
		public int Age;

		[JsonProperty("height")]
		public double Height;

		[JsonProperty("married")]
		public bool Married;

		[JsonProperty("skills")]
		public List<Skills> PersonSkills;

		public class Skills
		{
			[JsonProperty("id")]
			public int Id;

			[JsonProperty("skill")]
			public string Skill;
		}
	}
}

