namespace ProjetoAllVoyaje.Models
{
    public class PacoteCliente
    {
        public Guid PacoteClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public Guid ClienteId { get; set; }
        public Guid PacoteViagemId { get; set; }
        public PacoteViagem? Pacote { get; set; }        
        public int QtdPessoas { get; set; }

    }
}
