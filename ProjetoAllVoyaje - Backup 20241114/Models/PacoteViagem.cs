using System.ComponentModel.DataAnnotations;

namespace ProjetoAllVoyaje.Models
{
    public class PacoteViagem 
    {
        public Guid PacoteViagemId { get; set; }
        public string NomePacote { get; set; }

        public string Hotel {  get; set; }
        

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public TipoPacote? tipopacote { get; set; }
        public Guid TipoPacoteId { get; set; }

        public DateOnly DataSaida { get; set; }
        public DateOnly DataRetorno { get; set; }

        [Display(Name = "Valor Total")]
        public decimal Preco { get; set; }
        
    }
}
