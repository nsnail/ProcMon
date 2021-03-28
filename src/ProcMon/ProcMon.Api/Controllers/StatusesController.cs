using Microsoft.AspNetCore.Mvc;
using ProcMon.Core.Domains;
using ProcMon.Core.Operations;
using ProcMon.Core.Utils;

namespace ProcMon.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StatusesController : ControllerBase, IOperations<StatusesDomain.StatusesItem>
	{
		private readonly StatusesOperations _statusesOperations = new(Const.STATUSES_FILE);

		[HttpPost]
		[Route(nameof(Add))]
		public RootDomain<StatusesDomain.StatusesItem> Add(StatusesDomain.StatusesItem item)
		{
			return _statusesOperations.Add(item);
		}

		[HttpDelete]
		[Route(nameof(Delete))]
		public RootDomain<StatusesDomain.StatusesItem> Delete(string guid)
		{
			return _statusesOperations.Delete(guid);
		}

		[HttpGet]
		[Route(nameof(GetAll))]
		public RootDomain<StatusesDomain.StatusesItem> GetAll()
		{
			return _statusesOperations.GetAll();
		}

		[HttpGet]
		[Route(nameof(GetSingle))]
		public StatusesDomain.StatusesItem GetSingle(string guid)
		{
			return _statusesOperations.GetSingle(guid);
		}

		[HttpPut]
		[Route(nameof(Set))]
		public RootDomain<StatusesDomain.StatusesItem> Set(StatusesDomain.StatusesItem item)
		{
			return _statusesOperations.Set(item);
		}

		[HttpPut]
		[Route(nameof(Write))]
		public RootDomain<StatusesDomain.StatusesItem> Write(RootDomain<StatusesDomain.StatusesItem> root)
		{
			return _statusesOperations.Write(root);
		}
		 

	}
}