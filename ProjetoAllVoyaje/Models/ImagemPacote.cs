using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoAllVoyaje.Models
{
    public class ImagemPacote
    {
        public Guid ImagemPacoteId { get; set; }
        public Guid PacoteViagemId { get; set; }
        public PacoteViagem? PacoteViagem { get; set; }
        public string? URL { get; set; }

        [NotMapped] // Para evitar que isso seja mapeado para o banco de dados
        public IFormFile? Imagem { get; set; }
    }
}
