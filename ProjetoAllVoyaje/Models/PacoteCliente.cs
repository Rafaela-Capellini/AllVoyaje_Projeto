namespace ProjetoAllVoyaje.Models
{
    public class PacoteCliente
    {
        public Guid PacoteClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public Guid ClienteId { get; set; }
        public TipoPacote? TipoPacote { get; set; }
        public Guid TipoPacoteId { get; set; }
        public int QtdPessoas { get; set; }

    }
}
