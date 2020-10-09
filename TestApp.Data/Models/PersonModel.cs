using Microsoft.Azure.Cosmos.Table;
using System;
using TestApp.Data.Helpers;

namespace TestApp.Data.Models
{
	public class PersonModel : TableEntity
	{
		public PersonModel()
		{
			PartitionKey = Constants.TableDBConstant.TEST_APP_PARTITION;
			RowKey = Guid.NewGuid().ToString();
		}
	}
}

