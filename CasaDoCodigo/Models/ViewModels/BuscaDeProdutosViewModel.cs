using System.Collections.Generic;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public BuscaDeProdutosViewModel(IList<Produto> produtos, IList<Categoria> categorias,string pesquisa = "",string erro="")
        {
            this.Produtos = produtos;
            this.Categorias = categorias;
            this.Pesquisa = pesquisa;
            this.Pesquisa_Erro = erro;

        }
        public string Pesquisa { get; set; }
        public string Pesquisa_Erro { get; set; }
        public IList<Categoria> Categorias { get; set; }
        public IList<Produto> Produtos { get; set; }


        public BuscaDeProdutosViewModel()
        {

        }
    }
}
