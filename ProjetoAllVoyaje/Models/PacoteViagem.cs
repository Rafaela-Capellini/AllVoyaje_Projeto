namespace ProjetoAllVoyaje.Models
{
    public class PacoteViagem 
    {
        public Guid PacoteViagemId { get; set; }
        public string Descricao { get; set; }
        public TipoPacote tipopacote { get; set; }
        public Guid TipoPacoteId { get; set; }
        public decimal Preco { get; set;}
    }
}
