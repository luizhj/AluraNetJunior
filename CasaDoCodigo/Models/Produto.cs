using System.ComponentModel.DataAnnotations;

namespace CasaDoCodigo.Models
{
    public class Produto : BaseModel
    {
        public Produto()
        {

        }

        [Required]
        public string Codigo { get; private set; }

        [Required]
        public string Nome { get; private set; }

        [Required]
        public decimal Preco { get; private set; }

        public virtual Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }

        public Produto(string codigo, string nome, decimal preco,int _categoria)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
            this.CategoriaId = _categoria;
        }
    }
}
