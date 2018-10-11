using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public BuscaDeProdutosViewModel(IList<Produto> produtos, IList<Categoria> categorias)
        {
            this.Produtos = produtos;
            this.Categorias = categorias;
        }

        public IList<Categoria> Categorias { get; set; }
        public IList<Produto> Produtos { get; set; }
    }
}
