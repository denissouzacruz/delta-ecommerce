﻿using Delta.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Models
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        Task<Categoria> ObterCategoriaProduto(Guid id);
    }
}
