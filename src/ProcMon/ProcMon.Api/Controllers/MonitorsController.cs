using Microsoft.AspNetCore.Mvc;
using ProcMon.Core.Domains;
using ProcMon.Core.Operations;
using ProcMon.Core.Utils;

namespace ProcMon.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MonitorsController : ControllerBase, IOperations<MonitorsDomain.MonitorsItem>
	{
		private readonly MonitorOperations _monitorOperations = new MonitorOperations(Const.MONITORS_FILE);

		[HttpPost]
		[Route(nameof(Add))]
		public RootDomain<MonitorsDomain.MonitorsItem> Add(MonitorsDomain.MonitorsItem item)
		{
			return _monitorOperations.Add(item);
		}

		[HttpDelete]
		[Route(nameof(Delete))]
		public RootDomain<MonitorsDomain.MonitorsItem> Delete(string guid)
		{
			return _monitorOperations.Delete(guid);
		}

		[HttpGet]
		[Route(nameof(GetAll))]
		public RootDomain<MonitorsDomain.MonitorsItem> GetAll()
		{
			return _monitorOperations.GetAll();
		}

		[HttpGet]
		[Route(nameof(GetSingle))]
		public MonitorsDomain.MonitorsItem GetSingle(string guid)
		{
			return _monitorOperations.GetSingle(guid);
		}

		[HttpPut]
		[Route(nameof(Set))]
		public RootDomain<MonitorsDomain.MonitorsItem> Set(MonitorsDomain.MonitorsItem item)
		{
			return _monitorOperations.Set(item);
		}

		[HttpPut]
		[Route(nameof(Write))]
		public RootDomain<MonitorsDomain.MonitorsItem> Write(RootDomain<MonitorsDomain.MonitorsItem> root)
		{
			return _monitorOperations.Write(root);
		}
		 
	}
}