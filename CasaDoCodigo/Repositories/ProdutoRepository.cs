using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        //public IList<Produto> GetProdutos() => dbSet.ToList();
        public IList<Produto> GetProdutos(ModelStateDictionary _modelState,string pesquisa = "")
        {
            // se recebeu parametro de pesquisa e a pesquisa retornou a lista e a lista contem algo
            if (!string.IsNullOrEmpty(pesquisa))
            {
                if (dbSet.Where(p => p.Nome.ToUpper().Contains(pesquisa.ToUpper()) || p.Categoria.Nome.ToUpper().Contains(pesquisa.ToUpper())).ToList() is IList<Produto> retorno && retorno.Count > 0)
                {
                    // retorna a lista
                    return retorno;
                }
                _modelState.AddModelError("Pesquisa_Produto","Não foram encontrados produtos com o termo pesquisado.");
            }

            // retorna todos
            return dbSet.ToList();
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                Categoria categoria;

                if (contexto.Set<Categoria>().Where(p => p.Nome == livro.Categoria).FirstOrDefault() is Categoria _categoria)
                {
                    categoria = _categoria;
                }
                else
                {
                    categoria = new Categoria(livro.Categoria);
                    contexto.Set<Categoria>().Add(categoria);
                    contexto.SaveChanges();
                }

                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria.Id));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }
}
