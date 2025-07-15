using System.Threading.Tasks;
using MyTraining1101Demo.Security.Recaptcha;

namespace MyTraining1101Demo.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
