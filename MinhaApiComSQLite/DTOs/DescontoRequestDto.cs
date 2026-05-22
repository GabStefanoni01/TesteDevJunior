using System.ComponentModel.DataAnnotations;

namespace MinhaApiComSQLite.DTOs
{
    public class DescontoRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
    }
}
