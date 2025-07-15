using System.Threading.Tasks;
using Abp.Dependency;

namespace MyTraining1101Demo.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}