using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ProcMon.Core.Domains
{
	[DataContract]
	public class RootDomain<T> where T : class
	{
		[DataMember] public List<T> Items { get; set; }
	}
}