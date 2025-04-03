using Delta.Business.Data;
using Delta.Business.Models;
using Delta.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Infra.Repository
{
    public class VendedorRepository : Repository<Vendedor>
    {
        public VendedorRepository(DeltaDbContext deltaDbContext) : base(deltaDbContext)
        {
                
        }
    }
    
}
