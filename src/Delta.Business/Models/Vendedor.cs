using Delta.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Models
{
    public class Vendedor: EntityBase
    {
        public IEnumerable<Produto>? Produtos { get; set; }
    }
}
