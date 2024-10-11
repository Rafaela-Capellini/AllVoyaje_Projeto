namespace ProjetoAllVoyaje.Models
{
    public class OpcoesDatas 
    {
        public Guid OpcoesDatasId { get; set; }
        public DateOnly DataSaida { get; set; }
        public DateOnly DataRetorno { get; set; }
        public PacoteViagem PacoteViagem { get; set; }
        public Guid PacoteViagemId { get; set; }
        public decimal Preco { get; set; }
    }
}
