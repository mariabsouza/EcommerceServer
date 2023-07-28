namespace API.Models
{
    public class Produto
    {
        public  int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int Estoque { get; set; }
        public  float Valor { get; set; }
        public string Foto { get; set; }
    }
}
