using System.Threading.Tasks;

namespace MyTraining1101Demo.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}