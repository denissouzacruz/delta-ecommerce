using System.ComponentModel.DataAnnotations;

namespace Delta.Api.Models
{
    public class Vendedor
    {
        public Vendedor()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
    }
}
