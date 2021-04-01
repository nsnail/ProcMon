using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProcMon.Core.Domains;
using ProcMon.Core.Utils;

namespace ProcMon.Core.Operations
{
	public class StatusesOperations : ILogger, IOperations<StatusesDomain.StatusesItem>
	{
		private readonly string _filePath;

		public StatusesOperations(string filePath)
		{
			_filePath = filePath;
		}


		public RootDomain<StatusesDomain.StatusesItem> Add(StatusesDomain.StatusesItem item)
		{
			return Add(GetAll(), item);
		}


		public RootDomain<StatusesDomain.StatusesItem> Delete(string guid)
		{
			var root = GetAll();
			root.Items ??= new List<StatusesDomain.StatusesItem>();
			root.Items.RemoveAll(x => x.Guid == guid);
			Write(root);
			return root;
		}


		public RootDomain<StatusesDomain.StatusesItem> GetAll()
		{
			return File.ReadAllText(_filePath).SerializeJsonDe<StatusesDomain.Root>();
		}


		public StatusesDomain.StatusesItem GetSingle(string guid)
		{
			return GetAll().Items.SingleOrDefault(x => x.Guid == guid);
		}

		public RootDomain<StatusesDomain.StatusesItem> Set(StatusesDomain.StatusesItem item)
		{
			var root = GetAll();
			root.Items ??= new List<StatusesDomain.StatusesItem>();

			var fileItem = root.Items.SingleOrDefault(x => x.Guid == item.Guid);
			if (fileItem == null) return Add(root, item);

			fileItem.CopyFrom(item);
			Write(root);
			return root;
		}


		public RootDomain<StatusesDomain.StatusesItem> Write(RootDomain<StatusesDomain.StatusesItem> root)
		{
			try {
				File.WriteAllText(_filePath, root.Json(true));
			} catch (IOException exp) {
				Log.Error(exp);
			}

			return root;
		}

		private RootDomain<StatusesDomain.StatusesItem> Add(RootDomain<StatusesDomain.StatusesItem> root,
			StatusesDomain.StatusesItem item)
		{
			root.Items ??= new List<StatusesDomain.StatusesItem>();
			if (root.Items.Any(x => x.Guid == item.Guid)) throw new InvalidOperationException("guid重复添加");
			root.Items.Add(item);
			Write(root);
			return root;
		}

		public Logger Log => new(typeof(StatusesOperations));
	}
}