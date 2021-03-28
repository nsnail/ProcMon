using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProcMon.Core.Domains
{
	[DataContract]
	public class MonitorsDomain
	{
		[DataContract]
		public class MonitorsItem
		{
			/// <summary>
			/// </summary>
			[DataMember]
			public bool CheckExe { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public bool CheckLog { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public string ExePath { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public string Guid { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public string LogPath { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public int LogTimeout { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public string StartArgs { get; set; }

			/// <summary>
			/// </summary>
			[DataMember]
			public string WorkFolder { get; set; }
		}

		[DataContract]
		public class Root : RootDomain<MonitorsItem>
		{

			public List<MonitorsItem> Items { get; set; }
		}
	}
}