using CasaDoCodigo.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroRepository
    {
        Task<Cadastro> Update(int cadastroId, Cadastro novoCadastro);
    }

    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task<Cadastro> Update(int cadastroId, Cadastro novoCadastro)
        {
            if (dbSet.Where(c => c.Id == cadastroId).SingleOrDefault() is Cadastro cadastroDB)
            {
                cadastroDB.Update(novoCadastro);
                await contexto.SaveChangesAsync();
                return cadastroDB;
            }
            else
            {
                throw new ArgumentNullException("cadastro");
            }
        }

    }
}

