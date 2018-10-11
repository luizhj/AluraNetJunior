using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public IList<Categoria> GetCategorias() => dbSet.ToList();

        public async Task SaveCategorias(List<Categoria> categorias)
        {
            foreach (var item in categorias)
            {
                if (!dbSet.Where(p => p.Id == item.Id).Any())
                {
                    dbSet.Add(new Categoria(item.Id, item.Nome));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }
}
