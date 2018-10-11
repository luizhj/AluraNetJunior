using CasaDoCodigo.Models;
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

        public IList<Produto> GetProdutos() => dbSet.ToList();

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
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco,categoria.Id));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }
}
