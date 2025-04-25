using AutoMapper;
using Delta.Api.Models;
using Delta.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delta.Api.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriasController(IProdutoRepository produtoRepository,
                                    ICategoriaRepository categoriaRepository,
                                    IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Models.Categoria>> GetCategoria(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaModel = _mapper.Map<Models.Categoria>(categoria);
            return categoriaModel;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaRepository.ObterTodos();
            var categoriasModel = _mapper.Map<IEnumerable<Models.Categoria>>(categorias);

            return categoriasModel.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Models.Categoria>> PostCategoria(Models.Categoria categoria)
        {
            var categoriaModel = _mapper.Map<Business.Models.Categoria>(categoria);
            await _categoriaRepository.Adicionar(categoriaModel);

            return CreatedAtAction(nameof(GetCategoria), new { Id = categoria.Id }, categoria);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutCategoria(Guid id, Models.Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();

            var categoriaBd = await _categoriaRepository.ObterPorId(id);
            if (categoriaBd == null)
                return NotFound();

            _mapper.Map(categoria, categoriaBd);
            await _categoriaRepository.Atualizar(categoriaBd);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            var categoriaBd = _categoriaRepository.ObterPorId(id);
            if (_categoriaRepository.ObterPorId(id) == null)
            {
                return NotFound();
            }
            await _categoriaRepository.Remover(id);
            return NoContent();
        }
    }
}
