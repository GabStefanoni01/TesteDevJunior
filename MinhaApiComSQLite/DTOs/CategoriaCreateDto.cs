using System.ComponentModel.DataAnnotations;

namespace MinhaApiComSQLite.DTOs
{
    public class CategoriaCreateDto
    {
        [Required(ErrorMessage = "O nome da categoria e obrigatorio.")]
        [MinLength(2, ErrorMessage = "O nome da categoria deve ser descritivo.")]
        public string Nome { get; set; } = string.Empty;
    }
}
