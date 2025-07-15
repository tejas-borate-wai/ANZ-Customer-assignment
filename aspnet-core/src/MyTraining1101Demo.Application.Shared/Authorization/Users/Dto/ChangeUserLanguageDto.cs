using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
