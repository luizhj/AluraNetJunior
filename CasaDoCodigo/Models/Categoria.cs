namespace CasaDoCodigo.Models
{
    public class Categoria : BaseModel
    {
        public string Nome { get; set; }

        public Categoria(int _id, string _nome)
        {
            this.Id = _id;
            this.Nome = _nome;
        }

        public Categoria(string _nome) => this.Nome = _nome;

        public Categoria()
        {

        }
    }
}
