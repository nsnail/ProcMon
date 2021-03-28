using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProcMon.Core.Domains;
using ProcMon.Core.Utils;

namespace ProcMon.Core.Operations
{
	public class MonitorOperations : IOperations<MonitorsDomain.MonitorsItem>
	{
		private readonly string _filePath;

		public MonitorOperations(string filePath)
		{
			_filePath = filePath;
		}

		public RootDomain<MonitorsDomain.MonitorsItem> Add(MonitorsDomain.MonitorsItem item)
		{
			return Add(GetAll(), item);
		}

		public RootDomain<MonitorsDomain.MonitorsItem> Delete(string guid)
		{
			var root = GetAll();
			root.Items ??= new List<MonitorsDomain.MonitorsItem>();
			root.Items.RemoveAll(x => x.Guid == guid);
			Write(root);
			return root;
		}

		public RootDomain<MonitorsDomain.MonitorsItem> GetAll()
		{
			return File.ReadAllText(_filePath).SerializeJsonDe<MonitorsDomain.Root>();
		}

		public MonitorsDomain.MonitorsItem GetSingle(string guid)
		{
			return GetAll().Items.SingleOrDefault(x => x.Guid == guid);
		}

		public RootDomain<MonitorsDomain.MonitorsItem> Set(MonitorsDomain.MonitorsItem item)
		{
			var root = GetAll();
			root.Items ??= new List<MonitorsDomain.MonitorsItem>();

			var fileItem = root.Items.SingleOrDefault(x => x.Guid == item.Guid);
			if (fileItem == null) return Add(item);
			fileItem.CopyFrom(item);
			Write(root);
			return root;
		}

		public RootDomain<MonitorsDomain.MonitorsItem> Write(RootDomain<MonitorsDomain.MonitorsItem> root)
		{
			File.WriteAllText(_filePath, root.Json(true));
			return root;
		}


		private RootDomain<MonitorsDomain.MonitorsItem> Add(RootDomain<MonitorsDomain.MonitorsItem> root,
			MonitorsDomain.MonitorsItem item)
		{
			root.Items ??= new List<MonitorsDomain.MonitorsItem>();
			if (root.Items.Any(x => x.Guid == item.Guid)) throw new InvalidOperationException("guid重复添加");
			root.Items.Add(item);
			Write(root);
			return root;
		}
	}
}