using System.Threading.Tasks;
using MyTraining1101Demo.Views;
using Xamarin.Forms;

namespace MyTraining1101Demo.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
