using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProcMon.Core.Domains
{
	[DataContract]
	public class StatusesDomain
	{
		public enum StatusVals
		{
			正常 = 1,
			进程退出 = 2,
			日志超时 = 3
		}

		[DataContract]
		public class Root : RootDomain<StatusesItem>
		{
			public List<StatusesItem> Items { get; set; }
		}

		[DataContract]
		public class StatusesItem
		{
			/// <summary>
			/// </summary>
			[DataMember]
			public string Guid { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public StatusVals Status { get; set; }
		}
	}
}