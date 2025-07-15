using System.Threading.Tasks;

namespace MyTraining1101Demo.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}