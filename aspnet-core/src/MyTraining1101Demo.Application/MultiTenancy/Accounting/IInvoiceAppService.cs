using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using MyTraining1101Demo.MultiTenancy.Accounting.Dto;

namespace MyTraining1101Demo.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
