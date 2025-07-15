using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Security;

namespace MyTraining1101Demo.Net.Emailing
{
    public class MyTraining1101DemoSmtpEmailSenderConfiguration : SmtpEmailSenderConfiguration
    {
        public MyTraining1101DemoSmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
        {

        }

        public override string Password => SimpleStringCipher.Instance.Decrypt(GetNotEmptySettingValue(EmailSettingNames.Smtp.Password));
    }
}