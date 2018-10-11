using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;
        private readonly ICategoriaRepository categoriaRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository,
            ICategoriaRepository categoriaRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
            this.categoriaRepository = categoriaRepository;
        }

        public IActionResult Carrossel(ModelStateDictionary modelState)
        {
            return View(produtoRepository.GetProdutos(_modelState: modelState));
        }

        public IActionResult BuscaDeProdutos(ModelStateDictionary _modelState, BuscaDeProdutosViewModel _viewmodel = null)
        {
            var pesquisa = string.Empty;

            if (_viewmodel is BuscaDeProdutosViewModel)
            {
                pesquisa = _viewmodel.Pesquisa;
            }

            var produtos = produtoRepository.GetProdutos(_modelState, pesquisa);
            var categorias = categoriaRepository.GetCategorias();
            var erro = string.Empty;
            if (!_modelState.IsValid)
            {
                var modelErrors = new List<string>();
                foreach (var modelState in _modelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        erro = modelError.ErrorMessage;
                        break;
                    }
                }
            }
            var viewmodel = new BuscaDeProdutosViewModel(produtos, categorias, pesquisa, erro);
            return View(viewmodel);
        }

        public async Task<IActionResult> Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                await pedidoRepository.AddItem(codigo);
            }

            Pedido taskPedido = await pedidoRepository.GetPedido();
            List<ItemPedido> itens = taskPedido.Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
            return base.View(carrinhoViewModel);
        }

        public async Task<IActionResult> Cadastro()
        {
            var pedido = await pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            return View(pedido.Cadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                return View(await pedidoRepository.UpdateCadastro(cadastro));
            }
            return RedirectToAction("Cadastro");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<UpdateQuantidadeResponse> UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            return await pedidoRepository.UpdateQuantidade(itemPedido);
        }
    }
}
