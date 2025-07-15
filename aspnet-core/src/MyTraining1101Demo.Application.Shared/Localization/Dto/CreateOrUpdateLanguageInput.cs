using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}