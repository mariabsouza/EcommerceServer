namespace API.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public int QtdItens { get; set; }
        public int PagamentoId { get; set; }
        public string StatusPagamento { get; set; }
        public int IdUser { get; set; }
    }
}
