using System.ComponentModel.DataAnnotations;

namespace MinhaApiComSQLite.DTOs
{
    public class ProdutoUpdateDto
    {
        [Required(ErrorMessage = "O nome do produto e obrigatorio.")]
        [MinLength(2, ErrorMessage = "O nome do produto deve ser descritivo.")]
        public string Nome { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O preco deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A categoria e obrigatoria.")]
        public int CategoriaId { get; set; }
    }
}
