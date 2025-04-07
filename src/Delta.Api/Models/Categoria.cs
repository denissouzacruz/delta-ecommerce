using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Delta.Api.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter no mínimo {2} caracteres e no máximo {1}", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter no mínimo {2} caracteres e no máximo {1}", MinimumLength = 2)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
