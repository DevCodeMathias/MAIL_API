using System.ComponentModel.DataAnnotations;

namespace mail_api.Domain.DTO
{
    public class cepRequest
    {

        [Required(ErrorMessage = "Cep is Required")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "InvalidCep: Invalid format. Expected format: xxxxx-xxx.")]

        public string Cep { get; set; }
    }
}
