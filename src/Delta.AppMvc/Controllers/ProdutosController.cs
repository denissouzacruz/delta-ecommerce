using AutoMapper;
using Delta.AppMvc.ViewModel;
using Delta.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Delta.AppMvc.Controllers
{
    [Route("/", Order =0)]
    [Route("gestao-produtos", Order =1)]
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                    ICategoriaRepository categoriaRepository,
                                    IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var produtoCategoria = await _produtoRepository.ObterProdutoCategoria();
            var produtoViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtoCategoria);
            return View(produtoViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("detalhes/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {

            var produto = await _produtoRepository.ObterProdutoCategoria(id);

            if (produto == null)
            {
                return NotFound();
            }

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
            return View(produtoViewModel);
        }

        [HttpGet]
        [Route("novo")]
        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaRepository.ObterTodos();
            var categoriaViewModel = _mapper.Map<IEnumerable<CategoriaViewModel>>(categorias);
            var produtoViewModel = new ProdutoViewModel() { Categorias = categoriaViewModel };
            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("novo")]
        public async Task<IActionResult> Create([FromForm] ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var produto = _mapper.Map<Produto>(produtoViewModel);
                produto.VendedorId = new Guid("791adda4-683f-44ab-970c-e02872851873");
                await _produtoRepository.Adicionar(produto);

                return RedirectToAction(nameof(Index));
            }
            return View(produtoViewModel);
        }

        [HttpGet]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var produto = await _produtoRepository.ObterProdutoCategoria(id);
            if (produto == null)
            {
                return NotFound();
            }

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
            var categorias = await _categoriaRepository.ObterTodos();
            var categoriasVM = _mapper.Map<IEnumerable<CategoriaViewModel>>(categorias);
            produtoViewModel.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos()); 
            return View(produtoViewModel);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,[FromForm] ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var produto = _mapper.Map<Produto>(produtoViewModel);
                    await _produtoRepository.Atualizar(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produtoViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produtoViewModel);
        }

        [HttpGet]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var produto = await _produtoRepository.ObterProdutoCategoria(id);
            if (produto == null)
            {
                return NotFound();
            }

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto != null)
            {
                await _produtoRepository.Remover(produto.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(Guid id)
        {
            var retorno = _produtoRepository.ObterPorId(id);
            return retorno != null;
        }
    }
}
