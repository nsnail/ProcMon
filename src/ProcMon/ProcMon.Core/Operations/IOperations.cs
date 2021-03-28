using ProcMon.Core.Domains;

namespace ProcMon.Core.Operations
{
	public interface IOperations<T> where T : class
	{
		RootDomain<T> Add(T item);
		RootDomain<T> Delete(string guid);
		RootDomain<T> GetAll();
		T GetSingle(string guid);
		RootDomain<T> Set(T item);

		RootDomain<T> Write(RootDomain<T> root);
	}
}