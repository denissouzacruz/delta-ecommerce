using System.ComponentModel.DataAnnotations;

namespace Delta.AppMvc.ViewModel
{
    public class VendedorViewModel
    {
        public VendedorViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
